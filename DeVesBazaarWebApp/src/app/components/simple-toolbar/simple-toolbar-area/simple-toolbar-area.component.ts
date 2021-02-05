import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-simple-toolbar-area',
  templateUrl: './simple-toolbar-area.component.html',
  styleUrls: ['./simple-toolbar-area.component.scss']
})
export class SimpleToolbarAreaComponent {

  @Input() justifyContent = 'right';
  @Input() showBorder = false;
  @Input() showBackground = false;

  constructor() { }

  get isJustifyContentLeft(): boolean {
    return this.justifyContent
        ? this.justifyContent.toLowerCase() === 'left'
        : false;
  }

  get isJustifyContentCentert(): boolean {
    return this.justifyContent
        ? this.justifyContent.toLowerCase() === 'center'
        : false;
  }

  get isJustifyContentRight(): boolean {
    return this.justifyContent
        ? this.justifyContent.toLowerCase() === 'right'
        : false;
  }
}
