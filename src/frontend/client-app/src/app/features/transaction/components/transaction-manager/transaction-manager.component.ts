import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Transaction } from 'src/app/core/entities/transaction';
import { TransactionType } from 'src/app/core/enums/TransactionType';
import { TransactionService } from '../../services/transaction-service/transaction.service';
import { CreateDialogComponent } from '../create-dialog/create-dialog.component';
import { EditDialogComponent } from '../edit-dialog/edit-dialog.component';

@Component({
  selector: 'app-transaction-manager',
  templateUrl: './transaction-manager.component.html',
  styleUrls: ['./transaction-manager.component.css']
})
export class TransactionManagerComponent implements OnInit {

  transactions: Transaction[] = [];
  displayedColumns: string[] = ['itemName', 'itemAmount', 'transactionType', 'notes', 'edit', 'delete']
  constructor(
    private _transactionService: TransactionService,
    private dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.reloadData();
    this._transactionService.changed$.subscribe(_ => this.reloadData());
  }

  reloadData(): void {
    this._transactionService.getAll().subscribe(transactions => {
      this.transactions = transactions;
    })
  }

  getTransactionTypeName(type: TransactionType): string 
  {
    return TransactionType[type];
  }

  create(): void 
  {
    this.dialog.open(CreateDialogComponent);
  }

  edit(transaction: Transaction) : void 
  {
    const dialogConfig = new MatDialogConfig()
    dialogConfig.data = { transactionToUpdate: transaction}

    this.dialog.open(EditDialogComponent, dialogConfig)
  }

  delete(transaction: Transaction): void
  {
    this._transactionService.delete(transaction.id).subscribe();
  }
}
