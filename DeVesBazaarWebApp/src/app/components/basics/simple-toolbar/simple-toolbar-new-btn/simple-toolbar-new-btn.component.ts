import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-simple-toolbar-new-btn',
  templateUrl: './simple-toolbar-new-btn.component.html',
  styleUrls: ['./simple-toolbar-new-btn.component.scss']
})
export class SimpleToolbarNewBtnComponent implements OnInit {

  @Input() showText = false;
  @Input() styleClass: string;

  constructor() { }

  ngOnInit(): void {
  }

}
