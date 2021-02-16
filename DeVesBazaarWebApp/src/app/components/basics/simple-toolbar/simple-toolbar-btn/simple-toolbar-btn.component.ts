import { Component, Input } from '@angular/core';

@Component({
  selector: 's-toolbar-btn',
  templateUrl: './simple-toolbar-btn.component.html',
  styleUrls: ['./simple-toolbar-btn.component.scss']
})
export class SimpleToolbarBtnComponent {

  @Input() iconLeft: string;
  @Input() caption: string;
  @Input() iconRight: string;
  @Input() styleClass: string;

  constructor() { }
}
