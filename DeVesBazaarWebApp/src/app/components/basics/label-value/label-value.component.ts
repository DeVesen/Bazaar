import { Component, Input, OnInit } from '@angular/core';

export declare type LabelValueType = 'vertical' | 'horizontal';

@Component({
  selector: 'app-label-value',
  templateUrl: './label-value.component.html',
  styleUrls: ['./label-value.component.scss']
})
export class LabelValueComponent implements OnInit {

  @Input() type: LabelValueType = 'vertical';
  @Input() title: string;
  @Input() value: string;
  @Input() defaultValue: string;

  constructor() { }

  ngOnInit(): void {
  }

}
