import { Component, OnInit } from '@angular/core';
import { StorageModel } from 'src/app/core/entities/storage';
import { StorageService } from '../../services/storage-service/storage.service';

@Component({
  selector: 'app-storage-manager',
  templateUrl: './storage-manager.component.html',
  styleUrls: ['./storage-manager.component.css']
})
export class StorageManagerComponent implements OnInit {

  storages: StorageModel[] = [];

  constructor(
    private _storageService: StorageService
  ) { }

  ngOnInit(): void {
    this._storageService.getAll().subscribe(storages => {
      this.storages = storages;
    })
  }

}
