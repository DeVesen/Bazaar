import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 's-add-btn',
  templateUrl: './simble-add-button.component.html',
  styleUrls: ['./simble-add-button.component.scss']
})
export class SimbleAddButtonComponent implements OnInit {

  @Input() caption: string;
  @Input() category: string;
  @Input() iconLeft: string = 'pi pi-plus';
  @Input() iconRight: string;
  @Input() styleClass: string;
  @Input() disabled: boolean = false;
  
  @Input() styleType: string = 'outlined';
  @Input() sizeType: string = 'normal';

  constructor() { }

  ngOnInit(): void {
  }

}
