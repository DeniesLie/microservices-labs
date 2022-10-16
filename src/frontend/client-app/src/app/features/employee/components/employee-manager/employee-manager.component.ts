import { Component, OnInit } from '@angular/core';
import { Employee } from 'src/app/core/entities/employee';

@Component({
  selector: 'app-employee-manager',
  templateUrl: './employee-manager.component.html',
  styleUrls: ['./employee-manager.component.css']
})
export class EmployeeManagerComponent implements OnInit {

  employees: Employee[] = [];

  constructor() { }

  ngOnInit(): void {
  }

}
