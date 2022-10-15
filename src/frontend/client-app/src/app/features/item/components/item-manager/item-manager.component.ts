import { Component, OnInit } from '@angular/core';
import { Item } from "src/app/core/entities/item";
import { ItemService } from "../../services/item.service";

@Component({
  selector: 'app-item-manager',
  templateUrl: './item-manager.component.html',
  styleUrls: ['./item-manager.component.css']
})
export class ItemManagerComponent implements OnInit {

  items: Item[] = [];

  constructor(
    private _itemService: ItemService
  ) { }

  ngOnInit(): void {
    this._itemService.getAll().subscribe(items => {
      this.items = items;
    })
  }

}
