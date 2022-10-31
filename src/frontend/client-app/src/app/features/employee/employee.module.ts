import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeeManagerComponent } from './components/employee-manager/employee-manager.component';
import { EmployeeRoutingModule } from './employee-routing.module';
import { CreateDialogComponent } from './components/create-dialog/create-dialog.component';
import { EditDialogComponent } from './components/edit-dialog/edit-dialog.component';
import {MatButtonModule} from '@angular/material/button';
import {MatTable, MatTableModule} from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field'
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input'
import { MatDialogModule } from '@angular/material/dialog'
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    EmployeeManagerComponent,
    CreateDialogComponent,
    EditDialogComponent
  ],
  imports: [
    CommonModule,
    EmployeeRoutingModule,
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
export class EmployeeModule { }
