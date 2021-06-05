import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { TabsetComponent } from 'ngx-bootstrap/tabs/public_api';
import { IProperty } from '../IProperty.interface';

@Component({
  selector: 'app-add-property',
  templateUrl: './add-property.component.html',
  styleUrls: ['./add-property.component.css']
})
export class AddPropertyComponent implements OnInit {
  @ViewChild('Form') addPropertyForm : NgForm;
  @ViewChild('formTabs') formTabs: TabsetComponent;

  propertiesTypes : Array<string> = ['Housing', 'Apartment', 'Duplex'];
  furnishTypes : Array<string> = ['Fully', 'Semi', 'Unfurnished'];
  propertyView : IProperty ={
    Id:null,
    Name:'',
    Price:null,
    SellRent:null,
    Type:null
  };

  constructor(private router:Router) { }

  ngOnInit() {
  }

  OnBack(){
    this.router.navigate(['/']);
  }

  onSubmit(){
    console.log("Win");
    console.log(this.addPropertyForm);
  }

  selectTab(tabId: number) {
    console.log(tabId);
    this.formTabs.tabs[tabId].active = true;
  }

}
