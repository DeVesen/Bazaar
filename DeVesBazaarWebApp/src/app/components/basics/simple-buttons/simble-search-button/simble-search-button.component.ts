import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 's-search-btn',
  templateUrl: './simble-search-button.component.html',
  styleUrls: ['./simble-search-button.component.scss']
})
export class SimbleSearchButtonComponent implements OnInit {

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
