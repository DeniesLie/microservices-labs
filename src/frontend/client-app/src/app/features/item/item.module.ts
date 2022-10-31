import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ItemManagerComponent } from './components/item-manager/item-manager.component';
import { ItemRoutingModule } from "./item-routing.module";
import { EditDialogComponent } from './components/edit-dialog/edit-dialog.component';
import { CreateDialogComponent } from './components/create-dialog/create-dialog.component';
import {MatButtonModule} from '@angular/material/button';
import { MatTableModule} from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field'
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input'
import { MatDialogModule } from '@angular/material/dialog'
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    ItemManagerComponent,
    EditDialogComponent,
    CreateDialogComponent
  ],
  imports: [
    CommonModule,
    ItemRoutingModule,
    MatButtonModule,
    MatTableModule,
    MatIconModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    MatDialogModule,
    FormsModule, ReactiveFormsModule
  ]
})
export class ItemModule { }
