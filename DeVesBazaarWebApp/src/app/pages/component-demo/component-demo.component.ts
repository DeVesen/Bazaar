import { Component, OnInit, AfterViewInit, Input } from '@angular/core';

@Component({
  selector: 'app-component-demo',
  templateUrl: './component-demo.component.html',
  styleUrls: ['./component-demo.component.scss']
})
export class ComponentDemoComponent implements OnInit, AfterViewInit {

  constructor() {
  }

  ngOnInit(): void {
  }
  
  ngAfterViewInit(): void {
  }
}
