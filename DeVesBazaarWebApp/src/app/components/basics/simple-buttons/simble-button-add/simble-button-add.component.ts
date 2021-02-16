import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 's-btn-add',
  templateUrl: './simble-button-add.component.html',
  styleUrls: ['./simble-button-add.component.scss']
})
export class SimbleButtonAddComponent implements OnInit {

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
