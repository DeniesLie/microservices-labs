import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { Transaction, TransactionCreate } from 'src/app/core/entities/transaction';
import { TransactionType } from 'src/app/core/enums/TransactionType';
import { TransactionService } from '../../services/transaction-service/transaction.service';
import { FormBuilder, FormControlStatus } from '@angular/forms'; 

@Component({
  selector: 'app-create-dialog',
  templateUrl: './create-dialog.component.html',
  styleUrls: ['./create-dialog.component.css']
})
export class CreateDialogComponent implements OnInit {

  dataForm: FormGroup = this.fb.group({
    itemId: ['', Validators.required],
    amount: [1, Validators.min(1)],
    storageId: ['', Validators.required],
    type: [TransactionType.Income],
    notes: ['']
  })

  constructor(
    private transactionService: TransactionService,
    private dialogRef: MatDialogRef<CreateDialogComponent>,
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
  }

  create(){
    const transaction: TransactionCreate = this.dataForm.getRawValue();
    this.transactionService.create(transaction)
    .subscribe(_ => {
      this.dialogRef.close()
    })
  }
}
