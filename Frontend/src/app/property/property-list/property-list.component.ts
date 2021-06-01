import { Component} from '@angular/core';

@Component({
  selector: 'app-property-list',
  templateUrl: './property-list.component.html',
  styleUrls: ['./property-list.component.css']
})
export class PropertyListComponent {

  properties: Array<any> = [
    {
      "Id":1,
      "Name":"Birla House",
      "Type":"House",
      "Price":12000
    },
    {
      "Id":2,
      "Name":"Eron House",
      "Type":"House",
      "Price":5000
    },
    {
      "Id":3,
      "Name":"Cow House",
      "Type":"House",
      "Price":232000
    },{
      "Id":4,
      "Name":"Xon House",
      "Type":"House",
      "Price":9000
    }
  ]
}