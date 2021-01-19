import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-expansion-panel',
  templateUrl: './expansion-panel.component.html',
  styleUrls: ['./expansion-panel.component.scss']
})
export class ExpansionPanelComponent implements OnInit {

  @Input() hideToggle = false;
  @Input() headerTitle: string;
  @Input() headerDesc: string;

  constructor() { }

  ngOnInit(): void {
  }

}
