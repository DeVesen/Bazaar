import { Injectable } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MessageBoxComponent } from 'src/app/components/basics/message-box/message-box.component';
import { SellerManagemendDialogComponent } from 'src/app/components/seller/seller-managemend-dialog/seller-managemend-dialog.component';
import { SellerApiService } from '../seller-api-service/seller-api.service';

@Injectable({
  providedIn: 'root'
})
export class SellerUiActionsService {
  
  private _dialogRef: MatDialogRef<any>;

  constructor(
    public _dialog: MatDialog,
    private _sellerApi: SellerApiService) { }


  public newSeller(): void {
    this.openDialog(SellerManagemendDialogComponent, null);
  }
  
  public updateSellerUi(number: number): void {
    this._sellerApi.get(number).then(sellerItem => {
      this.openDialog(SellerManagemendDialogComponent, sellerItem);
    });
  }

  public deleteSellerUi(number: number): void {
    const dlgData = {
      title: 'Verkäufer löschen ...',
      message: `Verkäufer ${number} wirklich löschen?`,
      buttons: MessageBoxComponent.DELETE_CANCEL_BTN
    };
    this.openDialog(MessageBoxComponent, dlgData).then(dlgResult => {
      if (dlgResult === 'btnDelete') {
        this._sellerApi.remove(number);
      }
    });
  }
  
  public navigateToArticels(number: number): void {
    console.log('navigateToArticels', number);
  }


  public closeActualDialog(): void {
    if (this._dialogRef) {
      this._dialogRef.close();
    }
  }
  

  private openDialog(component: any, data: any): Promise<any> {
    this.closeActualDialog();
    this._dialogRef = this._dialog.open(component, {
      data: data,
      maxWidth: '95vw',
      maxHeight: '95vh'
    });

    return this._dialogRef.afterClosed()
                          .toPromise();
  }
}
