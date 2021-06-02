import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from "rxjs/operators";
import { Observable } from 'rxjs';
import { IProperty } from '../property/IProperty.interface';

@Injectable({
  providedIn: 'root'
})
export class HousingService {

  constructor(private http:HttpClient) { }

  getAllProperties(): Observable<IProperty[]>{
    return this.http.get('data/properties.json').pipe(
      map(data => {
        const jsonData = JSON.stringify(data)
        const propertiesArray: Array<IProperty> = JSON.parse(jsonData);;
        return propertiesArray;
      })
    );
  }
  // getAllProperties() : Observable<IProperty[]>{
  //   return this.http.get('data/properties.json').pipe(
  //     map(data=>{
  //       const propetiesArray : Array<IProperty> = [];
  //       for (const id in data) {
  //         if(data.hasOwnProperty(id))
  //         {
  //           propetiesArray.push(data[id]);
  //         }
  //       }
        
  //       return propetiesArray;
  //     })
  //   )
  // }
}

