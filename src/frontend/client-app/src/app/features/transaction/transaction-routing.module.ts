import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { TransactionManagerComponent } from './components/transaction-manager/transaction-manager.component';

const routes: Routes = [
  {
    path: '',
    component: TransactionManagerComponent,
  }
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class TransactionRoutingModule { }
