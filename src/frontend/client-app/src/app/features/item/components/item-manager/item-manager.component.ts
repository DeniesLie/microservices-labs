import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Item, ItemCreate, ItemUpdate } from "src/app/core/entities/item";
import { ItemService } from "../../services/item.service";
import { CreateDialogComponent } from '../create-dialog/create-dialog.component';
import { EditDialogComponent } from '../edit-dialog/edit-dialog.component';

@Component({
  selector: 'app-item-manager',
  templateUrl: './item-manager.component.html',
  styleUrls: ['./item-manager.component.css']
})
export class ItemManagerComponent implements OnInit {

  items: Item[] = [];
  displayedColumns: string[] = ['itemId', 'itemName', 'edit', 'delete']
  constructor(
    private _itemService: ItemService,
    private dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.reloadData();
    this._itemService.changed$.subscribe(_ => this.reloadData());
  }

  reloadData(): void {
    this._itemService.getAll().subscribe(items => {
      this.items = items;
    })
  }

  create(): void 
  {
    this.dialog.open(CreateDialogComponent);
  }

  edit(item: Item) : void 
  {
    const dialogConfig = new MatDialogConfig()
    dialogConfig.data = { itemToUpdate: item}

    this.dialog.open(EditDialogComponent, dialogConfig)
  }

  delete(item: Item): void
  {
    this._itemService.delete(item.id).subscribe();
  }
}
