import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { catchError, Observable, throwError } from "rxjs";
import { environment } from 'src/environments/environment';
import { Item } from "src/app/core/entities/item";

@Injectable({
  providedIn: 'root'
})
export class ItemService {

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  constructor(
    private _httpClient: HttpClient
  ) { }

  getAll(): Observable<Item[]> {
    const url = `${environment.apiUrl}/itemservice/api/items`;
    return this._httpClient.get<Item[]>(url).pipe(
      catchError(err => this.handleError('get messages', err))
    )
  }

  private handleError(method: string, err: any) {
    console.log(`Error while trying to ${method}. ${err}`);
    return throwError(err);
  }
}
