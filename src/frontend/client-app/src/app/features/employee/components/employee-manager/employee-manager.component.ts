import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Employee } from 'src/app/core/entities/employee';
import { EmployeeService } from '../../services/employee-service/employee.service';
import { CreateDialogComponent } from '../create-dialog/create-dialog.component';
import { EditDialogComponent } from '../edit-dialog/edit-dialog.component';

@Component({
  selector: 'app-employee-manager',
  templateUrl: './employee-manager.component.html',
  styleUrls: ['./employee-manager.component.css']
})
export class EmployeeManagerComponent implements OnInit {

  employees: Employee[] = [];
  displayedColumns: string[] = ['name', 'lastname', 'phonenumber', 'edit', 'delete']

  constructor(
    private _employeeService: EmployeeService,
    private dialog: MatDialog) {}

  ngOnInit(): void {
    this.reloadData();
    this._employeeService.changed$.subscribe(_ => this.reloadData());
  }

  reloadData(): void {
    this._employeeService.getByStorageId('eff0398b-0fff-4fc6-98e2-9da4982a2736') 
      .subscribe(employees => {
        this.employees = employees;
    })
  }

  create(): void 
  {
    this.dialog.open(CreateDialogComponent);
  }

  edit(employee: Employee) : void 
  {
    const dialogConfig = new MatDialogConfig()
    dialogConfig.data = { employeeToUpdate: employee}

    this.dialog.open(EditDialogComponent, dialogConfig)
  }

  delete(employee: Employee): void
  {
    this._employeeService.delete(employee.id).subscribe();
  }
}
