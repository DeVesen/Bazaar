import { AfterViewInit, Component, Inject, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ISalutation } from '../../../models/salutation';
import { ISeller } from '../../../models/seller';
import { SellerApiService } from '../../../services/seller-api-service/seller-api.service';
import { MatSnackBar } from '@angular/material/snack-bar';

export interface ISellerManagemendDialogResult {
  action: string;
  seller: ISeller;
}

@Component({
  selector: 'app-seller-managemend-dialog',
  templateUrl: './seller-managemend-dialog.component.html',
  styleUrls: ['./seller-managemend-dialog.component.scss']
})
export class SellerManagemendDialogComponent implements OnInit, AfterViewInit {

  isNewMode: boolean;
  isUpdateMode: boolean;
  title: string;

  sellerNumberControl = new FormControl('');
  sellerSalutationControl = new FormControl('');
  sellerLastNameControl = new FormControl('', [Validators.required]);
  sellerFirstNameControl = new FormControl('', [Validators.required]);
  sellerStreetControl = new FormControl('');
  sellerZipControl = new FormControl('');
  sellerTownControl = new FormControl('');
  sellerPhoneControl = new FormControl('');
  sellerEMailControl = new FormControl(''/*, [Validators.required, Validators.email]*/);

  salutations: ISalutation[];


  constructor(
    private _sellerApi: SellerApiService,
    private _snackBar: MatSnackBar,
    public dialogRef: MatDialogRef<SellerManagemendDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ISeller) {

      this.salutations = [
        { caption: 'Herr', value: 'Herr'},
        { caption: 'Frau', value: 'Frau'},
        { caption: 'Andere', value: 'Andere'}
      ];

      this.setDlgMode(this.data);
      this.writeDataToScreen(this.data);
  }

  get getData(): ISeller {
    return {
      id: this.sellerNumberControl.value,
      salutation: this.sellerSalutationControl.value,
      lastName: this.sellerLastNameControl.value,
      firstName: this.sellerFirstNameControl.value,
      street: this.sellerStreetControl.value,
      zip: this.sellerZipControl.value,
      town: this.sellerTownControl.value,
      phone: this.sellerPhoneControl.value,
      eMail: this.sellerEMailControl.value,
    };
  }


  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
  }

  onSaveClicked(): void {
    if (!this.triggerAllFormControl()) {
      return;
    }

    let createUpdateResult: Promise<any>;

    if (this.isNewMode) {
      createUpdateResult = this._sellerApi.create(this.getData);
    } else {
      createUpdateResult = this._sellerApi.update(this.getData);
    }

    createUpdateResult.then(() => {
      this.showAddOrUpdateSuccessSnackBarInfo(this.isNewMode ? 'added' : 'updated', this.getData);
      this.dialogRef.close();
    })
    .catch(() => this.showAddOrUpdateErrorSnackBarInfo(this.getData));
  }

  onCancelClicked(): void {
    this.dialogRef.close();
  }


  private setDlgMode(data: ISeller): void {

    this.isUpdateMode = data !== undefined && data !== null && data.id > 0;
    this.isNewMode = !this.isUpdateMode;

    this.title = this.isNewMode ? 'Neuer Händler ...' : 'Händler anpassen ...';
  }

  private writeDataToScreen(data: ISeller): void {
    this.sellerNumberControl.setValue(data?.id);
    this.sellerSalutationControl.setValue(data?.salutation);
    this.sellerLastNameControl.setValue(data?.lastName);
    this.sellerFirstNameControl.setValue(data?.firstName);
    this.sellerStreetControl.setValue(data?.street ?? null);
    this.sellerZipControl.setValue(data?.zip ?? null);
    this.sellerTownControl.setValue(data?.town ?? null);
    this.sellerPhoneControl.setValue(data?.phone ?? null);
    this.sellerEMailControl.setValue(data?.eMail ?? null);
  }


  private triggerAllFormControl(): boolean {
    this.sellerNumberControl.markAllAsTouched();
    this.sellerSalutationControl.markAllAsTouched();
    this.sellerLastNameControl.markAllAsTouched();
    this.sellerFirstNameControl.markAllAsTouched();
    this.sellerStreetControl.markAllAsTouched();
    this.sellerZipControl.markAllAsTouched();
    this.sellerTownControl.markAllAsTouched();
    this.sellerPhoneControl.markAllAsTouched();
    this.sellerEMailControl.markAllAsTouched();

    return this.sellerNumberControl.valid &&
           this.sellerSalutationControl.valid &&
           this.sellerLastNameControl.valid &&
           this.sellerFirstNameControl.valid &&
           this.sellerStreetControl.valid &&
           this.sellerZipControl.valid &&
           this.sellerTownControl.valid &&
           this.sellerPhoneControl.valid &&
           this.sellerEMailControl.valid;
  }


  public getErrorMessage(formCtrl: FormControl): string {
    if (formCtrl.hasError('required')) {
      return 'Das Feld darf nicht leer sein!';
    }
    if (formCtrl.hasError('email')) {
      return 'Kein korrektes E-Mail Format!';
    }
    if (formCtrl.hasError('min')) {
      return `Der wert muss größer ${formCtrl.getError('min').min} sein!`;
    }
    
    if (formCtrl.hasError('numberInUse')) {
      return `Nummer ${formCtrl.getError('numberInUse').actual} ist bereits vergeben!`;
    }
    if (formCtrl.hasError('numberNotInUse')) {
      return `Nummer ${formCtrl.getError('numberNotInUse').actual} ist nicht vergeben!`;
    }

    return ''
  }


  private showAddOrUpdateSuccessSnackBarInfo(status: string, data: ISeller): void {
    const statusText = status === 'added' ? 'angelegt' : 'aktuallisiert';
    const snackBarMsg = `${data.lastName} ${data.firstName} mit der Nummer ${data.id} ${statusText}`;

    this._snackBar.dismiss();
    const snackBarItem = this._snackBar.open(snackBarMsg, 'OK', { duration: 2500, });
    snackBarItem.onAction().subscribe(() => this._snackBar.dismiss());
  }

  private showAddOrUpdateErrorSnackBarInfo(data: ISeller): void {
    const statusText = status === 'added' ? 'anlegen' : 'aktuallisieren';
    const snackBarMsg = `Fehler beim ${statusText} von ${data.lastName} ${data.firstName} mit der Nummer ${data.id}`;

    this._snackBar.dismiss();
    const snackBarItem = this._snackBar.open(snackBarMsg, 'Fehler', { duration: 5000, });
    snackBarItem.onAction().subscribe(() => this._snackBar.dismiss());
  }
}
