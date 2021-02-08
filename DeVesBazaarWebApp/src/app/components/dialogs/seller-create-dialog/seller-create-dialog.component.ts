import { AfterViewInit, Component, ElementRef, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { Dialog } from 'primeng/dialog';
import { IDialogResult } from 'src/app/models/dialog-result';
import { IManufacturer } from 'src/app/models/manufacturer-model';
import { SellerApiService } from 'src/app/services/seller-api-service/seller-api.service';

@Component({
  selector: 'app-seller-create-dialog',
  templateUrl: './seller-create-dialog.component.html',
  styleUrls: ['./seller-create-dialog.component.scss']
})
export class SellerCreateDialogComponent implements OnInit, AfterViewInit {

  private MSG_PLEASEINPUT: string = 'Bitte Verkäufer eintragen ...';
  private MSG_FORCEINPUT: string = 'Bitte einen Verkäufer eintragen!';
  private MSG_PLEASEWAIT: string = 'Bitte warten ...';
  
  @ViewChild(Dialog) _dialog: Dialog;
  @ViewChild('dlgInput') _inputField: ElementRef;

  doShowDialog = false;
  inputValue: string;
  inputInfoText = this.MSG_PLEASEINPUT;
  inputAsError = false;
  waitForCreation = false;

  closeWasInformed = false;
  @Output() dlgClosed = new EventEmitter<IDialogResult<IManufacturer>>();

  constructor(private _sellerApi: SellerApiService) { }

  ngOnInit(): void {
  }
  
  ngAfterViewInit(): void {
  }


  onCreateNewEntry(): void {

    this.setDialogStatus(true, false, this.MSG_PLEASEWAIT);

    const result = this.validateInputForNewEntry(this.inputValue);
    if (!result.isValid) {
      this.setDialogStatus(false, true, result.infoTxt);
      return;
    }

    this._sellerApi.create({id: 0, name: this.inputValue})
                         .then(r => {
                           if (r.error) {
                            this.setDialogStatus(false, true, ...r.error.messageCode);
                           } else {
                            this.closeDialog(r.data);
                           }
                         });
  }
  
  onShow(): void {
    this.resetToInitial();
    this.focusInput();
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

  public closeDialog(resultEntry: IManufacturer): void {
    this.doShowDialog = false;
    this.informParentAboutClose({wasCanceled: false, data: resultEntry});
  }


  private validateInputForNewEntry(value: string): any {

    var infoTxt = '';
    var isValid = false;

    if (value && value.length > 0) {
      infoTxt = this.MSG_PLEASEINPUT;
      isValid = true;
    } else {
      infoTxt = this.MSG_FORCEINPUT;
      isValid = false;
    }

    return {
      isValid: isValid,
      infoTxt: infoTxt
    };
  }

  private setDialogStatus(enable: boolean, errorState: boolean, ...statusText: string[]): void {
    this.waitForCreation = enable;
    this.inputAsError = errorState;
    this.inputInfoText = statusText.length > 0 ? statusText[0] : '';

    this.focusInput();
  }

  private informParentAboutClose(data: IDialogResult<IManufacturer>): void {
    if (!this.closeWasInformed) {
      this.closeWasInformed = true;
      this.dlgClosed.emit(data);
    }
  }

  private resetToInitial(): void {
    this.waitForCreation = false;
    this.inputAsError = false;
    this.inputInfoText = this.MSG_PLEASEINPUT;
    this.inputValue = '';
  }

  private focusInput(): void {
    this._inputField.nativeElement.focus();
    this._inputField.nativeElement.select();
  }
}
