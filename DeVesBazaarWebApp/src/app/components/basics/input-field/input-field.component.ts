import { Component, Input, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';


export declare type InputFieldAppearance = 'legacy' | 'standard' | 'fill' | 'outline';
export declare type InputFieldType = 'text' | 'number';
export declare type InputFieldFloatLabelType = 'always' | 'never' | 'auto';


@Component({
  selector: 'app-input-field',
  templateUrl: './input-field.component.html',
  styleUrls: ['./input-field.component.scss']
})
export class InputFieldComponent implements OnInit {

  @Input() appearance: InputFieldAppearance = 'standard';
  @Input() inputType: InputFieldType = 'text';
  @Input() floatLabel: InputFieldFloatLabelType = 'auto';

  @Input() labelValue: string;
  @Input() placeholder: string;
  @Input() hintValue: string;
  @Input() errorValue: string;
  @Input() prefixValue: string;
  @Input() suffixValue: string;

  @Input() inputRightAlign = false;

  @Input() frmCtrl: FormControl;

  constructor() { }

  ngOnInit(): void {
  }
}
