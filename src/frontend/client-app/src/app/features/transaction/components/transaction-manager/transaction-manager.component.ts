import { Component, OnInit } from '@angular/core';
import { Transaction } from 'src/app/core/entities/transaction';
import { TransactionService } from '../../services/transaction-service/transaction.service';

@Component({
  selector: 'app-transaction-manager',
  templateUrl: './transaction-manager.component.html',
  styleUrls: ['./transaction-manager.component.css']
})
export class TransactionManagerComponent implements OnInit {

  transactions: Transaction[] = [];

  constructor(
    private _transactionService: TransactionService
  ) { }

  ngOnInit(): void {
    this._transactionService.getAll().subscribe(transactions => {
      this.transactions = transactions;
    })
  }

}
