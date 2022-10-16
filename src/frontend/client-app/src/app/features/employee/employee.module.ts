import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeeManagerComponent } from './components/employee-manager/employee-manager.component';
import { EmployeeRoutingModule } from './employee-routing.module';



@NgModule({
  declarations: [
    EmployeeManagerComponent
  ],
  imports: [
    CommonModule,
    EmployeeRoutingModule
  ]
})
export class EmployeeModule { }
