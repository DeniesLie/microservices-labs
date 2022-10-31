import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { ItemCreate } from 'src/app/core/entities/item';
import { ItemService } from '../../services/item.service';

@Component({
  selector: 'app-create-dialog',
  templateUrl: './create-dialog.component.html',
  styleUrls: ['./create-dialog.component.css']
})
export class CreateDialogComponent implements OnInit {

  dataForm: FormGroup = this.fb.group({
    name: ['', Validators.required]
  })

  constructor(
    private itemService: ItemService,
    private dialogRef: MatDialogRef<CreateDialogComponent>,
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
  }

  create() {
    const item: ItemCreate = this.dataForm.getRawValue();
    this.itemService.create(item)
      .subscribe(_ => {
        this.dialogRef.close()
      })
  }

}
