import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { StorageModel } from 'src/app/core/entities/storage';
import { CreateDialogComponent } from '../create-dialog/create-dialog.component';
import { EditDialogComponent } from '../edit-dialog/edit-dialog.component';
import { StorageService } from '../../services/storage-service/storage.service';

@Component({
  selector: 'app-storage-manager',
  templateUrl: './storage-manager.component.html',
  styleUrls: ['./storage-manager.component.css']
})
export class StorageManagerComponent implements OnInit {

  storages: StorageModel[] = [];
  displayedColumns: string[] = ['id', 'name', 'address', 'edit', 'delete']

  constructor(
    private _storageService: StorageService,
    private dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this._storageService.getAll().subscribe(storages => {
      this.storages = storages;
    })
    this._storageService.changed$.subscribe(_ => this.reloadData());
  }

  reloadData(): void {
    this._storageService.getAll().subscribe(storages => {
      this.storages = storages;
    })
  }

  create(): void 
  {
    this.dialog.open(CreateDialogComponent);
  }

  edit(storage: StorageModel) : void 
  {
    const dialogConfig = new MatDialogConfig()
    dialogConfig.data = { storageToUpdate: storage}

    this.dialog.open(EditDialogComponent, dialogConfig)
  }

  delete(storage: StorageModel): void
  {
    this._storageService.delete(storage.id).subscribe();
  }

}
