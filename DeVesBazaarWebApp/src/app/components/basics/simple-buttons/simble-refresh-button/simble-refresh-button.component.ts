import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 's-refresh-btn',
  templateUrl: './simble-refresh-button.component.html',
  styleUrls: ['./simble-refresh-button.component.scss']
})
export class SimbleRefreshButtonComponent implements OnInit {

  @Input() caption: string;
  @Input() category: string;
  @Input() iconLeft: string = 'pi pi-refresh';
  @Input() iconRight: string;
  @Input() styleClass: string;
  @Input() disabled: boolean = false;
  
  @Input() styleType: string = 'outlined';
  @Input() sizeType: string = 'normal';

  constructor() { }

  ngOnInit(): void {
  }

}
