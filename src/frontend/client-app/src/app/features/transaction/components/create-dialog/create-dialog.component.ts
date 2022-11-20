import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { Transaction, TransactionCreate } from 'src/app/core/entities/transaction';
import { TransactionType } from 'src/app/core/enums/TransactionType';
import { TransactionService } from '../../services/transaction-service/transaction.service';
import { FormBuilder, FormControlStatus } from '@angular/forms'; 
import { Item } from 'src/app/core/entities/item';
import { StorageModel } from 'src/app/core/entities/storage';
import { ItemService } from 'src/app/features/item/services/item.service';
import { StorageService } from 'src/app/features/storage/services/storage-service/storage.service';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-create-dialog',
  templateUrl: './create-dialog.component.html',
  styleUrls: ['./create-dialog.component.css']
})
export class CreateDialogComponent implements OnInit, OnDestroy {

  public items: Item[] = [];
  public storages: StorageModel[] = [];

  private unsubscribeAll: Subject<any>;

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
    private fb: FormBuilder,
    private itemService: ItemService,
    private storageService: StorageService
  ) { 
    this.unsubscribeAll = new Subject();
  }

  ngOnInit(): void {
    this.initForm();
  }

  ngOnDestroy(): void {
    this.unsubscribeAll.next(true);
  }

  create(){
    const transaction: TransactionCreate = this.dataForm.getRawValue();
    this.transactionService.create(transaction)
    .subscribe(_ => {
      this.dialogRef.close()
    })
  }

  private initForm() {
    this.initItems();
    this.initStorages();
  }

  private initItems() {
    this.itemService.getAll()
      .pipe(takeUntil(this.unsubscribeAll))
      .subscribe(items => {
        if (items) {
          this.items = items;
        }
      })
  }

  private initStorages() {
    this.storageService.getAll()
      .pipe(takeUntil(this.unsubscribeAll))
      .subscribe(storages => {
        if (storages) {
          this.storages = storages;
        }
      })
  } 

}
