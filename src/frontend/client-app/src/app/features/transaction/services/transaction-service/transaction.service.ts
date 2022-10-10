import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { Transaction } from 'src/app/core/entities/transaction';
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

  constructor(
    private _httpClient: HttpClient
  ) { }

  getAll(): Observable<Transaction[]> {
    const url = `http://localhost:4200/api/transactionservice/api/transactions`; 
    return this._httpClient.get<Transaction[]>(url).pipe(
      catchError(err => this.handleError('get messages', err))
    )
  }

  private handleError(method: string, err: any) {
    console.log(`Error while trying to ${method}. ${err}`);
    return throwError(err);
  }  
}
