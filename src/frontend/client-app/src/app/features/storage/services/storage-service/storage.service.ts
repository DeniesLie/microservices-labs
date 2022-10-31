import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, Subject, tap, throwError } from 'rxjs';
import { StorageCreateModel, StorageModel, StorageUpdateModel } from 'src/app/core/entities/storage';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class StorageService {

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };
  baseUrl = `${environment.apiUrl}/storageservice/api/storages`;
  changed$ : Subject<any> = new Subject<any>();

  constructor(
    private _httpClient: HttpClient
    ) { }
  
    getAll(): Observable<StorageModel[]> {
      return this._httpClient.get<StorageModel[]>(this.baseUrl).pipe(
        catchError(err => this.handleError('get storages', err))
      )
    }

    create(storage: StorageCreateModel): Observable<StorageModel> {
      const url = `${this.baseUrl}/Create`
      return this._httpClient.post<StorageModel>(url, storage, this.httpOptions)
        .pipe(
          tap(storage => this.changed$.next(storage)),
          catchError(err => this.handleError('create storage', err))
        )
    }
  
    update(storage: StorageUpdateModel): Observable<StorageModel> {
      const url = `${this.baseUrl}/Update`
      return this._httpClient.put<StorageModel>(url, storage, this.httpOptions).pipe(
        tap(storage => this.changed$.next(storage)),
        catchError(err => this.handleError('update storage', err)))
    }
  
    delete(storageId: string) {
      const url = `${this.baseUrl}/${storageId}`;
      return this._httpClient.delete(url).pipe(
        tap(obj => this.changed$.next(obj)),
        catchError(err => this.handleError('delete storage', err))
      );
    }
  
    private handleError(method: string, err: any) {
      console.log(`Error while trying to ${method}. ${err}`);
      return throwError(err);
    }  
}
