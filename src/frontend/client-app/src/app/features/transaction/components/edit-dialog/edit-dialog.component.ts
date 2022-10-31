import { Component, Inject, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { TransactionUpdate } from 'src/app/core/entities/transaction';
import { TransactionService } from '../../services/transaction-service/transaction.service';

@Component({
  selector: 'app-edit-dialog',
  templateUrl: './edit-dialog.component.html',
  styleUrls: ['./edit-dialog.component.css']
})
export class EditDialogComponent implements OnInit {

  transactionToUpdate: TransactionUpdate
  notesControl: FormControl

  constructor(
    private transactionService: TransactionService,
    private dialogRef: MatDialogRef<EditDialogComponent>,
    @Inject(MAT_DIALOG_DATA) data : any
  ) { 
    this.transactionToUpdate = 
    { 
      id: data.transactionToUpdate.id, 
      notes: data.transactionToUpdate.notes 
    };
    this.notesControl = new FormControl(data.transactionToUpdate.notes);
  }

  ngOnInit(): void {
  }

  update(): void {
    const newNotes = this.notesControl.getRawValue();
    this.transactionToUpdate.notes = newNotes;
    this.transactionService.update(this.transactionToUpdate)
      .subscribe(_ => this.dialogRef.close());
  }
}
