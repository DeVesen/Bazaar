import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

export interface IMessageBoxBtn {
  key: string;
  title: string;
  color?: string;
}
export interface IMessageBoxModel {
  title: string;
  message: string;
  buttons: IMessageBoxBtn[];
}


@Component({
  selector: 'app-message-box',
  templateUrl: './message-box.component.html',
  styleUrls: ['./message-box.component.scss']
})
export class MessageBoxComponent implements OnInit {

  constructor(
    public dialogRef: MatDialogRef<MessageBoxComponent>,
    @Inject(MAT_DIALOG_DATA) public data: IMessageBoxModel) {
      console.log(this.data);
    }

  ngOnInit(): void {
  }

  onBtnClicked(btnKey: string): void {
    this.dialogRef.close(btnKey);
  }


  
  public static OK_BTN: IMessageBoxBtn[] = [
    { key: 'btnOk', title: 'OK', color: 'primary' }
  ];
  public static DELETE_CANCEL_BTN: IMessageBoxBtn[] = [
    { key: 'btnDelete', title: 'Löschen', color: 'warn' },
    { key: 'btnCancel', title: 'Abbruch', color: 'primary' }
  ];
}
