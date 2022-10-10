import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { StorageModel } from 'src/app/core/entities/storage';

@Injectable({
  providedIn: 'root'
})
export class StorageService {

  constructor(
    private _httpClient: HttpClient
    ) { }
  
    getAll(): Observable<StorageModel[]> {
      const url = `http://localhost:4200/api/storageservice/api/storages`; 
      return this._httpClient.get<StorageModel[]>(url).pipe(
        catchError(err => this.handleError('get storages', err))
      )
    }
  
    private handleError(method: string, err: any) {
      console.log(`Error while trying to ${method}. ${err}`);
      return throwError(err);
    }  
}
