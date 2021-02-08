import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-simple-toolbar-search-btn',
  templateUrl: './simple-toolbar-search-btn.component.html',
  styleUrls: ['./simple-toolbar-search-btn.component.scss']
})
export class SimpleToolbarSearchBtnComponent implements OnInit {

  @Input() showText = false;
  @Input() styleClass: string;

  constructor() { }

  ngOnInit(): void {
  }

}
