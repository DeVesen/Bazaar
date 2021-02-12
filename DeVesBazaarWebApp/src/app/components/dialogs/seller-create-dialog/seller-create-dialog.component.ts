import { AfterViewInit, Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { IDialogResult } from 'src/app/models/dialog-result';
import { IManufacturer } from 'src/app/models/manufacturer-model';
import { ISeller } from '../../../models/seller-model';
import { SimpleInputComponent } from '../../primeng/simple-input/simple-input.component';
import { SellerApiService } from '../../../services/seller-api-service/seller-api.service';

@Component({
  selector: 'app-seller-create-dialog',
  templateUrl: './seller-create-dialog.component.html',
  styleUrls: ['./seller-create-dialog.component.scss']
})
export class SellerCreateDialogComponent implements OnInit, AfterViewInit {
  
  @ViewChild('sellerId') _sellerIdField: SimpleInputComponent;
  @ViewChild('sellerFirstName') _sellerFirstNameField: SimpleInputComponent;
  @ViewChild('sellerLastName') _sellerLastNameField: SimpleInputComponent;

  sellerNumber: string;
  sellerSalutation: string;
  sellerFirstname: string;
  sellerLastname: string;
  sellerPhone: string;
  sellerEmail: string;

  doShowDialog = false;
  waitForCreation = false;

  closeWasInformed = false;
  @Output() dlgClosed = new EventEmitter<IDialogResult<ISeller>>();

  constructor(private _sellerApi: SellerApiService) { }

  ngOnInit(): void {
  }
  
  ngAfterViewInit(): void {
  }


  async onCreateNewEntry(): Promise<void> {

    this.waitForCreation = true;

    if (await this.validateSellerIdField() && this.validateSellerFirstNameField() && this.validateSellerLastNameField()) {
      const sellerElement: ISeller = this.getActualSeller();
      const createResult = await this._sellerApi.create(sellerElement);
      if (createResult.data) {
        this.closeDialog(createResult.data);
      }

      console.log(createResult.error);
    }
    
    this.waitForCreation = false;
  }
  
  inputChange(inputKey: string): void {

    switch(inputKey) {
      case 'sellerId':
        {
          this.validateSellerIdField();
        }
        break;
      case 'sellerFirstName':
        {
          this.validateSellerFirstNameField();
        }
        break;
      case 'sellerLastName':
        {
          this.validateSellerLastNameField();
        }
        break;
    }
  }
  
  onShow(): void {
    this.resetToInitial();
    this.closeWasInformed = false;
  }

  onHide(): void {
    this.informParentAboutClose(null);
    this.resetToInitial();
  }

  onKeyUp($event: any): void {
    if ($event.key === 'Enter') {
      this.onCreateNewEntry();
    }
  }


  public showDialog(): void {
    this.doShowDialog = true;
    this.resetToInitial();
  }

  public closeDialog(resultEntry: ISeller): void {
    this.doShowDialog = false;
    this.informParentAboutClose({wasCanceled: false, data: resultEntry});
  }
  

  private resetToInitial(): void {
    this.waitForCreation = false;
    this.sellerNumber = undefined;
    this.sellerSalutation = undefined;
    this.sellerFirstname = undefined;
    this.sellerLastname = undefined;
    this.sellerPhone = undefined;
    this.sellerEmail = undefined;
  }

  private informParentAboutClose(data: IDialogResult<ISeller>): void {
    if (!this.closeWasInformed) {
      this.closeWasInformed = true;
      this.dlgClosed.emit(data);
    }
  }


  private async validateSellerIdField(): Promise<boolean> {
    this._sellerIdField.errorTxt = null;
    
    if (!this._sellerIdField.value || this._sellerIdField.value.length <= 0) {
      this._sellerIdField.errorTxt = 'Eine Verkäufernummer größer als 0!'
    } else if (+this._sellerIdField.value <= 0) {
      this._sellerIdField.errorTxt = 'Eine Verkäufernummer größer als 0!'
    }

    const numberExists = await this._sellerApi.idExist(+this._sellerIdField.value);
    if (numberExists.data) {
      this._sellerIdField.errorTxt = 'Bereits in gebrauch!'
    }

    return !this._sellerIdField.errorTxt;
  }
  private validateSellerFirstNameField(): boolean {
    this._sellerFirstNameField.errorTxt = null;
    
    if (!this._sellerFirstNameField.value || this._sellerFirstNameField.value.trim().length <= 0) {
      this._sellerFirstNameField.errorTxt = 'Bitte eintragen!'
    }

    return !this._sellerFirstNameField.errorTxt;
  }
  private validateSellerLastNameField(): boolean {
    this._sellerLastNameField.errorTxt = null;
    
    if (!this._sellerLastNameField.value || this._sellerLastNameField.value.trim().length <= 0) {
      this._sellerLastNameField.errorTxt = 'Bitte eintragen!'
    }

    return !this._sellerLastNameField.errorTxt;
  }

  private getActualSeller(): ISeller {
    return {
      id: +this.sellerNumber,
      salutation: this.sellerSalutation,
      firstName: this.sellerFirstname,
      lastName: this.sellerLastname,
      phone: this.sellerPhone,
      email: this.sellerEmail,
    };
  }
}
