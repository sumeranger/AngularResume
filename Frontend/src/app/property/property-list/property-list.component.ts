import { Component, OnInit} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IPropertyBase } from 'src/app/model/IPropertybase';
import { HousingService } from 'src/app/services/housing.service';

@Component({
  selector: 'app-property-list',
  templateUrl: './property-list.component.html',
  styleUrls: ['./property-list.component.css']
})
export class PropertyListComponent implements OnInit{

  SellRent = 1;
  properties: Array<IPropertyBase> = [];

  constructor(private route : ActivatedRoute, private housingServices:HousingService){}

  ngOnInit(): void{
    if(this.route.snapshot.url.toString()){
      this.SellRent = 2;
    }

    this.housingServices.getAllProperties(this.SellRent).subscribe(
      data=>{
        this.properties=data;
        const newProperty = JSON.parse(localStorage.getItem('newProp'));
        if(newProperty.SellRent == this.SellRent){
          this.properties = [newProperty,...this.properties];
        }
        console.log(data);
      }, error=>{
        console.log("httpserror");
        console.log(error);
      }
    );
  }
}