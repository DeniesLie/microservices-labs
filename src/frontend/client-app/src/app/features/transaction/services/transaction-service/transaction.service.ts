import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, pipe, Subject, tap, throwError } from 'rxjs';
import { Transaction, TransactionCreate, TransactionUpdate } from 'src/app/core/entities/transaction';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class TransactionService {

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };
  baseUrl = `${environment.apiUrl}/transactionservice/api/transactions`;
  changed$ : Subject<any> = new Subject<any>();

  constructor(
    private _httpClient: HttpClient
  ) { }

  getAll(): Observable<Transaction[]> {
    return this._httpClient.get<Transaction[]>(this.baseUrl).pipe(
      catchError(err => this.handleError('get transactions by storageId', err))
    )
  }

  create(transaction: TransactionCreate): Observable<Transaction> {
    return this._httpClient.post<Transaction>(this.baseUrl, transaction, this.httpOptions)
      .pipe(
        tap(transaction => this.changed$.next(transaction)),
        catchError(err => this.handleError('create transaction', err))
      )
  }

  update(transaction: TransactionUpdate): Observable<Transaction> {
    return this._httpClient.put<Transaction>(this.baseUrl, transaction, this.httpOptions).pipe(
      tap(transaction => this.changed$.next(transaction)),
      catchError(err => this.handleError('update transaction', err)))
  }

  delete(transactionId: string) {
    const url = `${this.baseUrl}/${transactionId}`;
    return this._httpClient.delete(url).pipe(
      tap(obj => this.changed$.next(obj)),
      catchError(err => this.handleError('delete transaction', err))
    );
  }

  private handleError(method: string, err: any) {
    console.log(`Error while trying to ${method}. ${err}`);
    return throwError(err);
  }  
}
