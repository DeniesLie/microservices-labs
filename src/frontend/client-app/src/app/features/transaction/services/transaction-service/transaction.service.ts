import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, pipe, throwError } from 'rxjs';
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

  constructor(
    private _httpClient: HttpClient
  ) { }

  getByStorageId(storageId: string): Observable<Transaction[]> {
    const url = `${this.baseUrl}/${storageId}`; 
    return this._httpClient.get<Transaction[]>(url).pipe(
      catchError(err => this.handleError('get transactions by storageId', err))
    )
  }

  create(transaction: TransactionCreate): Observable<Transaction> {
    return this._httpClient.post<Transaction>(this.baseUrl, transaction, this.httpOptions)
      .pipe(catchError(err => this.handleError('create transaction', err)))
  }

  updateNotes(transaction: TransactionUpdate): Observable<Transaction> {
    return this._httpClient.put<Transaction>(this.baseUrl, transaction, this.httpOptions)
      .pipe(catchError(err => this.handleError('update transaction', err)))
  }

  deleteNotes(transactionId: string) {
    const url = `${this.baseUrl}/${transactionId}`;
    return this._httpClient.delete(url)
      .pipe(catchError(err => this.handleError('delete transaction', err)))
  }

  private handleError(method: string, err: any) {
    console.log(`Error while trying to ${method}. ${err}`);
    return throwError(err);
  }  
}
