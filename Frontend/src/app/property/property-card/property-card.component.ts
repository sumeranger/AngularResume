import { Component, Input } from '@angular/core';
import { ipropertybase } from 'src/app/model/ipropertybase';

@Component({
  selector: 'app-property-card',
  templateUrl: './property-card.component.html',
  styleUrls: ['./property-card.component.css']
})
export class PropertyCardComponent {
  @Input() property: ipropertybase;
  @Input() hideIcon: boolean;
}
