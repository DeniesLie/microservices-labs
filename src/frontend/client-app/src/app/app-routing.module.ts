import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: 'transactions',
    loadChildren: () => import('./features/transaction/transaction.module').then(m => m.TransactionModule)
  },
  {
    path: 'storages',
    loadChildren: () => import('./features/storage/storage.module').then(m => m.StorageModule)
  },
  {
    path: 'items',
    loadChildren: () => import('./features/item/item.module').then(m => m.ItemModule)
  },
  {
    path: 'employees',
    loadChildren: () => import('./features/employee/employee.module').then(m => m.EmployeeModule)
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
