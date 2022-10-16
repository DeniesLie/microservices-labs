import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ItemManagerComponent } from "./components/item-manager/item-manager.component";

const routes: Routes = [
  {
    path: '',
    component: ItemManagerComponent,
  }
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class ItemRoutingModule { }
