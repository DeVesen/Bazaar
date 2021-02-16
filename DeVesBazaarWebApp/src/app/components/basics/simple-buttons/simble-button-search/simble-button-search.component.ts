import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 's-btn-search',
  templateUrl: './simble-button-search.component.html',
  styleUrls: ['./simble-button-search.component.scss']
})
export class SimbleButtonSearchComponent implements OnInit {

  @Input() caption: string;
  @Input() category: string;
  @Input() iconLeft: string = 'pi pi-search';
  @Input() iconRight: string;
  @Input() styleClass: string;
  @Input() disabled: boolean = false;
  
  @Input() styleType: string = 'outlined';
  @Input() sizeType: string = 'normal';

  constructor() { }

  ngOnInit(): void {
  }

}
