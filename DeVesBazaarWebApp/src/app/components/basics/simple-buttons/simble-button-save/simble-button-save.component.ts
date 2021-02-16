import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 's-btn-save',
  templateUrl: './simble-button-save.component.html',
  styleUrls: ['./simble-button-save.component.scss']
})
export class SimbleButtonSaveComponent implements OnInit {

  @Input() caption: string = 'Speichern';
  @Input() iconLeft: string = 'pi pi-save';
  @Input() iconRight: string;
  @Input() styleClass: string;
  @Input() disabled: boolean = false;
  
  @Input() category: string = 'primary';
  @Input() styleType: string = 'outlined';
  @Input() sizeType: string = 'normal';

  constructor() { }

  ngOnInit(): void {
  }

}
