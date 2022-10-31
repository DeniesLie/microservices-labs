import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { StorageUpdateModel } from 'src/app/core/entities/storage';
import { StorageService } from '../../services/storage-service/storage.service';
import { CreateDialogComponent } from '../create-dialog/create-dialog.component';

@Component({
  selector: 'app-edit-dialog',
  templateUrl: './edit-dialog.component.html',
  styleUrls: ['./edit-dialog.component.css']
})
export class EditDialogComponent implements OnInit {

  dataForm: FormGroup

  constructor(
    private storageService: StorageService,
    private dialogRef: MatDialogRef<CreateDialogComponent>,
    private fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA) data : any
  ) { 
    this.dataForm = this.fb.group({
      id: [data.storageToUpdate.id],
      address: [data.storageToUpdate.address],
      name: [data.storageToUpdate.name]
    })
  }

  ngOnInit(): void {
  }

  update() {
    const storage: StorageUpdateModel = this.dataForm.getRawValue();
    this.storageService.update(storage)
      .subscribe(_ => {
        this.dialogRef.close()
      })
  }
}
