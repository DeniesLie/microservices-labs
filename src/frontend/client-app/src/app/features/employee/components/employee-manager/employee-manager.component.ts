import { Component, OnInit } from '@angular/core';
import { Employee } from 'src/app/core/entities/employee';
import { EmployeeService } from '../../services/employee-service/employee.service';

@Component({
  selector: 'app-employee-manager',
  templateUrl: './employee-manager.component.html',
  styleUrls: ['./employee-manager.component.css']
})
export class EmployeeManagerComponent implements OnInit {

  employees: Employee[] = [];

  constructor(private _employeeService: EmployeeService) {}

  ngOnInit(): void {
    this._employeeService.getByStorageId(1).subscribe(employees=>{this.employees = employees})
  }

}
