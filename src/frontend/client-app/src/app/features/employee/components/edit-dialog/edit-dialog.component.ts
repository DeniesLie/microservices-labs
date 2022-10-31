import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { EmployeeUpdate } from 'src/app/core/entities/employee';
import { EmployeeService } from '../../services/employee-service/employee.service';
import { CreateDialogComponent } from '../create-dialog/create-dialog.component';

@Component({
  selector: 'app-edit-dialog',
  templateUrl: './edit-dialog.component.html',
  styleUrls: ['./edit-dialog.component.css']
})
export class EditDialogComponent implements OnInit {

  dataForm: FormGroup

  constructor(
    private employeeService: EmployeeService,
    private dialogRef: MatDialogRef<CreateDialogComponent>,
    private fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA) data : any
  ) { 
    this.dataForm = this.fb.group({
      id: [data.employeeToUpdate.id],
      name: [data.employeeToUpdate.name],
      lastname: [data.employeeToUpdate.lastname],
      phoneNumber: [data.employeeToUpdate.phoneNumber],
      positionId: [data.employeeToUpdate.positionId]
    })

  }

  ngOnInit(): void {
  }

  edit(){
    const employee: EmployeeUpdate = this.dataForm.getRawValue();
    this.employeeService.update(employee)
    .subscribe(_ => {
      this.dialogRef.close()
    })
  }

}
