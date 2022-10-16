import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ItemManagerComponent } from './components/item-manager/item-manager.component';
import { ItemRoutingModule } from "./item-routing.module";

@NgModule({
  declarations: [
    ItemManagerComponent
  ],
  imports: [
    CommonModule,
    ItemRoutingModule
  ]
})
export class ItemModule { }
