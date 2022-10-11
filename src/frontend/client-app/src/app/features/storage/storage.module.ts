import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StorageManagerComponent } from './components/storage-manager/storage-manager.component';
import { StorageRoutingModule } from './storages-routing.module';



@NgModule({
  declarations: [
    StorageManagerComponent
  ],
  imports: [
    CommonModule,
    StorageRoutingModule
  ]
})
export class StorageModule { }
