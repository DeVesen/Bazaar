import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SellerNummberValidatorDirective } from './seller-nummber-validator/seller-nummber-validator.directive';



@NgModule({
  declarations: [
    SellerNummberValidatorDirective
  ],
  imports: [
    CommonModule
  ],
  exports: [
    SellerNummberValidatorDirective
  ]
})
export class ValidatorsModule { }
