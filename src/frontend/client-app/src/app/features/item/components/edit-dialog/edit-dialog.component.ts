import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ItemUpdate } from 'src/app/core/entities/item';
import { ItemService } from '../../services/item.service';
import { CreateDialogComponent } from '../create-dialog/create-dialog.component';

@Component({
  selector: 'app-edit-dialog',
  templateUrl: './edit-dialog.component.html',
  styleUrls: ['./edit-dialog.component.css']
})
export class EditDialogComponent implements OnInit {

  dataForm: FormGroup

  constructor(
    private itemService: ItemService,
    private dialogRef: MatDialogRef<CreateDialogComponent>,
    private fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA) data : any
  ) { 
    this.dataForm = this.fb.group({
      id: [data.itemToUpdate.id],
      name: [data.itemToUpdate.name, Validators.required]
    })
  }

  ngOnInit(): void {
  }

  update() {
    const item: ItemUpdate = this.dataForm.getRawValue();
    this.itemService.update(item)
      .subscribe(_ => {
        this.dialogRef.close()
      })
  }
}
