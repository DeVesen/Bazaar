import { Component, OnInit, OnChanges, SimpleChanges, AfterViewInit, Input, Output, EventEmitter } from '@angular/core';

export interface ActionButton {
  key: string;
  icon: string;
}

@Component({
  selector: 'app-simple-input',
  templateUrl: './simple-input.component.html',
  styleUrls: ['./simple-input.component.scss']
})
export class SimpleInputComponent implements OnInit, AfterViewInit, OnChanges {

  isMouseOver = false;
  isFocused = false;

  @Input() type: string = 'text';
  @Input() value: string;
  @Input() placeholder: string;
  @Input() labelTxt: string;
  @Input() helpTxt: string;
  @Input() errorTxt: string;

  @Input() exclamationInCaseOfError = true;

  @Input() buttonsLeft: ActionButton[];
  @Input() buttonsRight: ActionButton[];

  @Output() valueChange = new EventEmitter<string>();
  @Output() clickBtnLeft = new EventEmitter<string>();
  @Output() clickBtnRight = new EventEmitter<string>();


  constructor() { }

  get showActive(): boolean {
    return (this.isMouseOver || this.isFocused || this.value ? true : false);
  }

  get showInActive(): boolean {
    return (!this.isMouseOver && !this.isFocused && !this.value);
  }

  get hasError(): boolean {
    return this.errorTxt && this.errorTxt.length > 0;
  }

  get infoTxt(): string {
    return this.errorTxt && this.errorTxt.length > 0 ? this.errorTxt : this.helpTxt;
  }

  
  ngOnInit(): void {
  }
  
  ngAfterViewInit(): void {
  }
  
  ngOnChanges(changes: SimpleChanges): void {
  }


  resetValue(): void {
    this.value = '';
    this.valueChange.emit('');
  }

  clickLeftBtn(btnKey: string): void {
    this.clickBtnLeft.emit(btnKey);
  }

  clickRightBtn(btnKey: string): void {
    this.clickBtnRight.emit(btnKey);
    
  }

  ngModelChange($event): void {
    this.valueChange.emit($event);
  }

  mouseEnter(): void {
    this.isMouseOver = true;
  }

  mouseLeave(): void {
    this.isMouseOver = false;
  }

  onFocusInEvent($event): void {
    this.isFocused = true;
  }

  onFocusOutEvent($event): void {
    this.isFocused = false;
  }
}
