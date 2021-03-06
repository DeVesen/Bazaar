import { Component, Input, OnInit, Output, EventEmitter, ElementRef, ViewChild, AfterViewInit } from '@angular/core';

@Component({
  selector: 'app-simple-toolbar-search-input',
  templateUrl: './simple-toolbar-search-input.component.html',
  styleUrls: ['./simple-toolbar-search-input.component.scss']
})
export class SimpleToolbarSearchInputComponent implements OnInit, AfterViewInit {

  @ViewChild('dlgInput') _inputField: ElementRef;

  @Input() value = '';
  @Input() showBtnText = false;
  @Input() inputIsShown = false;
  @Input() styleClass: string;

  @Output() valueChange = new EventEmitter<string>();
  @Output() inputIsShownChange = new EventEmitter<boolean>();
  @Output() doSearch = new EventEmitter<string>();
  @Output() keyupChange = new EventEmitter<any>();
  
  inputId: string;

  constructor() {
    this.inputId = `input_${new Date().getTime()}`;
  }

  ngOnInit(): void {
  }
  
  ngAfterViewInit(): void {
  }


  doShow(): void {
    this.inputIsShown = true;
    this.inputIsShownChange.emit(this.inputIsShown);

    setTimeout(() => {
      const inputRef = document.getElementById(this.inputId) as HTMLInputElement;
      if (inputRef) {
        inputRef.focus();
      }
    }, 100);
  }

  doHide(): void {
    this.inputIsShown = false;
    this.inputIsShownChange.emit(this.inputIsShown);
  }

  onDoSearch() {
    this.doSearch.emit(this.value);
  }

  modelChange($event): void {
    this.valueChange.emit($event);
  }

  onKeyup($event) {
    this.keyupChange.emit($event);
  }
}
