import { Component } from '@angular/core';

@Component({
  selector: 'app-component-demo',
  templateUrl: './component-demo.component.html',
  styleUrls: ['./component-demo.component.scss']
})
export class ComponentDemoComponent {
  
  constructor() { }

  writeConsole($event) {
    console.log($event);
  }
}
