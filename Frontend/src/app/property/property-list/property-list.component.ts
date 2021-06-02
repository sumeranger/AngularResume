import { Component, OnInit} from '@angular/core';
import { HousingService } from 'src/app/services/housing.service';
import { IProperty } from '../IProperty.interface';

@Component({
  selector: 'app-property-list',
  templateUrl: './property-list.component.html',
  styleUrls: ['./property-list.component.css']
})
export class PropertyListComponent implements OnInit{

  properties: Array<IProperty> = [];

  constructor(private housingServices:HousingService){}

  ngOnInit(): void{
    this.housingServices.getAllProperties().subscribe(
      data=>{
        this.properties=data;
        console.log(data);
      }, error=>{
        console.log("httpserror");
        console.log(error);
      }
    );
  }
}