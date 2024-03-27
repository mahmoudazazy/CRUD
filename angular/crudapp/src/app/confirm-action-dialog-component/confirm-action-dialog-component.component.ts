import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-confirm-action-dialog-component',
  templateUrl: './confirm-action-dialog-component.component.html',
  styleUrls: ['./confirm-action-dialog-component.component.css']
})

 
export class ConfirmActionDialogComponent implements OnInit {

  title: string;
  message: string;

  constructor(public dialogRef: MatDialogRef<Component>, @Inject(MAT_DIALOG_DATA) public data: DialogData) {
    this.title = data.title;
    this.message = data.message;
   }

  ngOnInit(): void {
  }

  onNoClick(): void {
    this.dialogRef.close(this.data.message);
  }

  onYesClick(): void {
    this.dialogRef.close(DialogResult.Yes);
  }
}


export interface DialogData {
  title: string;
  message: string;
}

export const DialogResult = {
  Yes : 'yes',
  No: 'no'
};