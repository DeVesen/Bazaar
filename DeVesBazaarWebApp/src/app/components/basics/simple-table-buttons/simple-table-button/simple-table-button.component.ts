import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 's-tbl-btn',
  templateUrl: './simple-table-button.component.html',
  styleUrls: ['./simple-table-button.component.scss']
})
export class SimpleTableButtonComponent implements OnInit {

  @Input() caption: string;
  @Input() category: string;
  @Input() iconLeft: string;
  @Input() iconRight: string;
  @Input() styleClass: string;
  @Input() disabled: boolean = false;
  
  @Input() styleType: string = 'outlined';
  @Input() sizeType: string = 'normal';

  constructor() { }

  ngOnInit(): void {
  }

}
