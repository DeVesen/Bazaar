import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-simple-toolbar-btn',
  templateUrl: './simple-toolbar-btn.component.html',
  styleUrls: ['./simple-toolbar-btn.component.scss']
})
export class SimpleToolbarBtnComponent {

  @Input() iconLeft: string;
  @Input() label: string;
  @Input() iconRight: string;
  @Input() showText = false;
  @Input() styleClass: string;
  // @Input() command: (event?: any) => void;

  constructor() { }

  // onBtnClick($event: any): void {
  //   if (this.command) {
  //     this.command($event);
  //   }
  // }
}
