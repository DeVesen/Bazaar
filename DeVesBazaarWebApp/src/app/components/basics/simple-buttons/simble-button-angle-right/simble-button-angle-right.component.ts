import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 's-btn-angle-right',
  templateUrl: './simble-button-angle-right.component.html',
  styleUrls: ['./simble-button-angle-right.component.scss']
})
export class SimbleButtonAngleRightComponent implements OnInit {

  @Input() caption: string;
  @Input() category: string;
  @Input() iconLeft: string = 'pi pi-angle-right';
  @Input() iconRight: string;
  @Input() styleClass: string;
  @Input() disabled: boolean = false;
  
  @Input() styleType: string = 'outlined';
  @Input() sizeType: string = 'normal';

  constructor() { }

  ngOnInit(): void {
  }

}
