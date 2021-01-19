import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-input-field-demo',
  templateUrl: './input-field-demo.component.html',
  styleUrls: ['./input-field-demo.component.scss']
})
export class InputFieldDemoComponent implements OnInit {

  simpleTxtCtrl = new FormControl('');
  simpleNumberCtrl = new FormControl('');

  simpleErrorTxtCtrl = new FormControl('', [Validators.required]);
  simpleErrorNumberCtrl = new FormControl('', [Validators.required, Validators.min(1)]);


  constructor() { }

  ngOnInit(): void {
    this.simpleErrorTxtCtrl.markAllAsTouched();
    this.simpleErrorNumberCtrl.markAllAsTouched();
  }


  getErrorMessage(frmCtrl: FormControl): string {
    if (frmCtrl.hasError('required')) {
      return 'You must enter a value';
    }
    if (frmCtrl.hasError('min')) {
      return `You must enter a value greater than ${frmCtrl.getError('min').min}`;
    }

    return null;
  }
  
}
