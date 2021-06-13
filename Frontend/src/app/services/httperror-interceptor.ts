import { HttpInterceptor, HttpEvent, HttpHandler, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of, throwError } from 'rxjs';
import { catchError, concatMap, retryWhen } from 'rxjs/operators';
import { ErrorCode } from '../enums/enums';
import { AlertifyService } from './alertify.service';

@Injectable({
    providedIn: "root"
})
export class HttpErrorInterceptorService implements HttpInterceptor {

    constructor(private alertify: AlertifyService) { }

    intercept(req: HttpRequest<any>, next: HttpHandler) {//}: Observable<HttpEvent<any>> {
        console.log("HTTP REQUEST START");
        return next.handle(req).pipe(
            retryWhen(error => this.retryRequest(error, 10)),
            catchError((error: HttpErrorResponse) => {
                const errorMessage = this.setError(error);
                console.log(error);
                this.alertify.error(errorMessage);
                return throwError(errorMessage);
            })
        );
    }

    // Retry the request in case of error
    retryRequest(error: Observable<unknown>, retryCount: number): Observable<unknown> {
        return error.pipe(
            concatMap((checkError: HttpErrorResponse, count: number) => {
                if (count < retryCount) {
                    switch (checkError.status) {
                        case ErrorCode.serverDown:
                            // case ErrorCode.unauthorised:
                            return of(checkError);
                        default:
                            break;
                    }
                }
                return throwError(checkError);
            })
        );
    }

    setError(error: HttpErrorResponse): string {
        let errorMessage = 'Unknown error occurred';
        if (error.error instanceof ErrorEvent) {
            //Client side error
            errorMessage = error.error.message;
        } else {
            //Server side error
            if (error.status != 0) {
                errorMessage = error.error.errorMessage;
            }//else server down
        }
        return errorMessage;
    }
}