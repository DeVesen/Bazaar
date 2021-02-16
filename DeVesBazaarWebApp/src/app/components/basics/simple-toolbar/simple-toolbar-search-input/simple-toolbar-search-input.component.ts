import { Component, Input, OnInit, Output, EventEmitter, AfterViewInit } from '@angular/core';

@Component({
  selector: 'app-simple-toolbar-search-input',
  templateUrl: './simple-toolbar-search-input.component.html',
  styleUrls: ['./simple-toolbar-search-input.component.scss']
})
export class SimpleToolbarSearchInputComponent implements OnInit, AfterViewInit {

  @Input() value = '';
  @Input() inputIsShown = false;
  @Input() escMeansClosing = true;
  @Input() styleClass: string;

  @Output() valueChange = new EventEmitter<string>();
  @Output() inputIsShownChange = new EventEmitter<boolean>();
  @Output() doSearch = new EventEmitter<string>();
  @Output() keyupChange = new EventEmitter<any>();
  
  inputId: string;

  constructor() {
    this.inputId = `stbsiv`;
  }

  ngOnInit(): void {
  }
  
  ngAfterViewInit(): void {
  }


  clickBtn(btnKey: string): void {
    switch(btnKey) {
      case 'close':
        this.doHide();
        break;
      case 'search':
        this.onDoSearch();
        break;
    }
  }

  doShow(): void {
    this.inputIsShown = true;
    this.inputIsShownChange.emit(this.inputIsShown);

    setTimeout(() => {
      const inputRef = document.getElementById(this.inputId) as HTMLInputElement;
      if (inputRef) {
        inputRef.focus();
      }
    }, 1000);
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
    if (this.escMeansClosing && $event.key === 'Escape') {
      this.doHide();
      return;
    }
    this.keyupChange.emit($event);
  }
}
