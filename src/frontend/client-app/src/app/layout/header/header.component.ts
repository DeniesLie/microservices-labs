import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor(
    private _router: Router
  ) { }

  ngOnInit(): void {
  }

  onGoToTransactions() {
    this._router.navigate(['transactions'])
  }

  onGoToStorages() {
    this._router.navigate(['storages'])
  }
}
