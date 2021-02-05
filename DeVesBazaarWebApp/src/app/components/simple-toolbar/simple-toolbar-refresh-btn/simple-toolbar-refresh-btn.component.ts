import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-simple-toolbar-refresh-btn',
  templateUrl: './simple-toolbar-refresh-btn.component.html',
  styleUrls: ['./simple-toolbar-refresh-btn.component.scss']
})
export class SimpleToolbarRefreshBtnComponent implements OnInit {

  @Input() showText = false;
  @Input() styleClass: string;

  constructor() { }

  ngOnInit(): void {
  }

}
