import { Directive, Input } from '@angular/core';
import { AsyncValidator, AbstractControl, ValidationErrors, NG_ASYNC_VALIDATORS } from '@angular/forms';
import { SellerApiService } from '../../services/seller-api-service/seller-api.service';

@Directive({
  selector: '[sellerNummberValidator][ngModel], [sellerNummberValidator][formControl]',
  providers: [
      {provide: NG_ASYNC_VALIDATORS, useExisting: SellerNummberValidatorDirective, multi: true}
  ]
})
export class SellerNummberValidatorDirective implements AsyncValidator {

  @Input() isRequired: boolean = true;
  @Input() isNewMode: boolean = true;

  constructor(private _sellerApi: SellerApiService) { }
  
  async validate(control: AbstractControl): Promise<ValidationErrors | null> {
        
    if (!control.value) {
      return this.isRequired ? { 'required': 'Field can not be null or empty!' } : null;
    }

    const sellerNummber = +(control.value ?? '');
    if (sellerNummber <= 0) {
      return { 'min': { min: 1, actual: sellerNummber} };
    }

    const idExists = await this._sellerApi.idExist(sellerNummber);
    return this.isNewMode
      ? (idExists ? { 'numberInUse': { actual: sellerNummber } } : null)
      : (!idExists ? { 'numberNotInUse': { actual: sellerNummber } } : null)
  }
}
