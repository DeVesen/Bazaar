import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 's-btn-refresh',
  templateUrl: './simble-button-refresh.component.html',
  styleUrls: ['./simble-button-refresh.component.scss']
})
export class SimbleButtonRefreshComponent implements OnInit {

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
