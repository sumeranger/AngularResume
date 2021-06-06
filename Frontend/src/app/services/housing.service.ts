import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from "rxjs/operators";
import { Observable } from 'rxjs';
import { IPropertyBase } from '../model/IPropertybase';
import { Property } from '../model/property';

@Injectable({
  providedIn: 'root'
})
export class HousingService {

  constructor(private http:HttpClient) { }

  getProperty(id:number){
    return this.getAllProperties().pipe(
      map(propertiesArray => {
        return propertiesArray.find(p =>p.Id === id);
      })
    );
  }

  getAllProperties(SellRent?: number): Observable<IPropertyBase[]>{
    return this.http.get('data/properties.json').pipe(
      map(data => {
        // const jsonData = JSON.stringify(data)
        // const propertiesArray: Array<IProperty> = JSON.parse(jsonData);;
        const propertiesArray : Array<IPropertyBase> = [];
        const localProperty = JSON.parse(localStorage.getItem('newProp'));
        if(localProperty){
          for (const id in localProperty) {
            if(SellRent){
              if (localProperty.hasOwnProperty(id) && localProperty[id].SellRent === SellRent) {
                propertiesArray.push(localProperty[id]);
              }
            }else{
              propertiesArray.push(localProperty[id]);
            }
          }
        }
        for (const id in data) {
          if(SellRent){
            if (data.hasOwnProperty(id) && data[id].SellRent === SellRent) {
              propertiesArray.push(data[id]);
            }
          }else{
            propertiesArray.push(data[id]);
          }
        }

        return propertiesArray;
      })
    );
  }
  
  addNewProperty(property : Property){
    let newProp = [property];
    if(localStorage.getItem('newProp')){
      newProp = [property,...JSON.parse(localStorage.getItem('newProp'))];
    }
    localStorage.setItem('newProp', JSON.stringify(newProp));
  }

  newPropID(){
    if(localStorage.getItem('PID')){
      localStorage.setItem('PID', String(localStorage.getItem('PID')+1));
      return +localStorage.getItem('PID');
    }else{
      localStorage.setItem('PID', '101');
      return 101;
    }
  }
}

