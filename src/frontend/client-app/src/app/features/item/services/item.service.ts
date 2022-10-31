import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { catchError, Observable, Subject, tap, throwError } from "rxjs";
import { environment } from 'src/environments/environment';
import { Item, ItemCreate, ItemUpdate } from "src/app/core/entities/item";

@Injectable({
  providedIn: 'root'
})
export class ItemService {

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };
  baseUrl = `${environment.apiUrl}/itemservice/api/items`;
  changed$ : Subject<any> = new Subject<any>();

  constructor(
    private _httpClient: HttpClient
  ) { }

  getAll(): Observable<Item[]> {
    return this._httpClient.get<Item[]>(this.baseUrl).pipe(
      catchError(err => this.handleError('get messages', err))
    )
  }

  create(item: ItemCreate): Observable<Item> {
    return this._httpClient.post<Item>(this.baseUrl, item, this.httpOptions)
      .pipe(
        tap(item => this.changed$.next(item)),
        catchError(err => this.handleError('create item', err))
      )
  }

  update(item: ItemUpdate): Observable<Item> {
    return this._httpClient.put<Item>(this.baseUrl, item, this.httpOptions).pipe(
      tap(item => this.changed$.next(item)),
      catchError(err => this.handleError('update item', err)))
  }

  delete(itemId: string) {
    const url = `${this.baseUrl}/${itemId}`;
    return this._httpClient.delete(url).pipe(
      tap(obj => this.changed$.next(obj)),
      catchError(err => this.handleError('delete item', err))
    );
  }

  private handleError(method: string, err: any) {
    console.log(`Error while trying to ${method}. ${err}`);
    return throwError(err);
  }
}
