import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TransactionManagerComponent } from './components/transaction-manager/transaction-manager.component';
import { TransactionRoutingModule } from './transaction-routing.module';
import {MatButtonModule} from '@angular/material/button';


@NgModule({
  declarations: [
    TransactionManagerComponent
  ],
  imports: [
    CommonModule,
    TransactionRoutingModule,
    MatButtonModule
  ]
})
export class TransactionModule { }
