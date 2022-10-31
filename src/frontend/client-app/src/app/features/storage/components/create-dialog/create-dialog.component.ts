import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { StorageCreateModel } from 'src/app/core/entities/storage';
import { StorageService } from '../../services/storage-service/storage.service';

@Component({
  selector: 'app-create-dialog',
  templateUrl: './create-dialog.component.html',
  styleUrls: ['./create-dialog.component.css']
})
export class CreateDialogComponent implements OnInit {

  dataForm: FormGroup = this.fb.group({
    name: ['', Validators.required],
    address: ['', Validators.required]
  })

  constructor(
    private storageService: StorageService,
    private dialogRef: MatDialogRef<CreateDialogComponent>,
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
  }

  create() {
    const storage: StorageCreateModel = this.dataForm.getRawValue();
    this.storageService.create(storage)
      .subscribe(_ => {
        this.dialogRef.close()
      })
  }
}
