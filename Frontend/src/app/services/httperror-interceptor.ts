import { HttpInterceptor, HttpEvent, HttpHandler, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AlertifyService } from './alertify.service';

@Injectable({
    providedIn: "root"
})
export class HttpErrorInterceptorService implements HttpInterceptor {

    constructor(private alertify: AlertifyService) { }

    intercept(req: HttpRequest<any>, next: HttpHandler) {//}: Observable<HttpEvent<any>> {
        console.log("HTTP REQUEST START");
        return next.handle(req).pipe(
            catchError((error: HttpErrorResponse) => {
                const errorMessage = this.setError(error);
                console.log(error);
                this.alertify.error(errorMessage);
                return throwError(errorMessage);
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