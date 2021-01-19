import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { ISeller } from 'src/app/models/seller';

@Component({
  selector: 'app-seller-selection-input-demo',
  templateUrl: './seller-selection-input-demo.component.html',
  styleUrls: ['./seller-selection-input-demo.component.scss']
})
export class SellerSelectionInputDemoComponent implements OnInit {
  
  sellerCtrl = new FormControl();

  constructor() { }

  ngOnInit(): void {
  }


  inputChange($event: ISeller): void {
    console.log('Seller-Ctrl', $event);
  }

}
