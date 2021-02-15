import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 's-angle-right-btn',
  templateUrl: './simble-angle-right-button.component.html',
  styleUrls: ['./simble-angle-right-button.component.scss']
})
export class SimbleAngleRightButtonComponent implements OnInit {

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
