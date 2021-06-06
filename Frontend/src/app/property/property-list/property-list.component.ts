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
  city = '';
  searchCity = '';
  sortByParam = '';
  sortDirection = 'asc';

  constructor(private route : ActivatedRoute, private housingServices:HousingService){}

  ngOnInit(): void{
    if(this.route.snapshot.url.toString()){
      this.SellRent = 2;
    }

    this.housingServices.getAllProperties(this.SellRent).subscribe(
      data=>{
        this.properties=data;
      }, error=>{
        console.log("httpserror");
        console.log(error);
      }
    );
  }

  OnCityFilter(){
    this.searchCity = this.city;
  }

  OnCityClearFilter(){
    this.searchCity = '';
    this.city = '';
  }

  OnSortDirection(){
    if(this.sortDirection === 'desc'){
      this.sortDirection = 'asc';
    }else{
      this.sortDirection = 'desc';
    }
  }
}