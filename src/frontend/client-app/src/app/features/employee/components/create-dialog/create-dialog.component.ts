import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { EmployeeCreate } from 'src/app/core/entities/employee';
import { EmployeeService } from '../../services/employee-service/employee.service';

@Component({
  selector: 'app-create-dialog',
  templateUrl: './create-dialog.component.html',
  styleUrls: ['./create-dialog.component.css']
})
export class CreateDialogComponent implements OnInit {

  dataForm: FormGroup = this.fb.group({
    name: ['', Validators.required],
    lastname: ['', Validators.min(1)],
    phoneNumber: ['', Validators.required],
    positionId: ['', Validators.required]
  })

  constructor(
    private employeeService: EmployeeService,
    private dialogRef: MatDialogRef<CreateDialogComponent>,
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
  }

  create(){
    const employee: EmployeeCreate = this.dataForm.getRawValue();
    this.employeeService.create(employee)
    .subscribe(_ => {
      this.dialogRef.close()
    })
  }
}
