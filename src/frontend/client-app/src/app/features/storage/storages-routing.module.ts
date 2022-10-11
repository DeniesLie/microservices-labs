import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { StorageManagerComponent } from './components/storage-manager/storage-manager.component';

const routes: Routes = [
  {
    path: '',
    component: StorageManagerComponent,
  }
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class StorageRoutingModule { }
