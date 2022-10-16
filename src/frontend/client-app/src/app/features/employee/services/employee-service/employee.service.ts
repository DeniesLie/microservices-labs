import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { Employee } from 'src/app/core/entities/employee';
import { catchError, Observable, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  constructor(
    private _httpClient: HttpClient
  ) { }

  getByStorageId(storageId: number): Observable<Employee[]> {
    const url = `${environment.apiUrl}/employeeservice/api/employees/GetByStorage/${storageId}`; 
    return this._httpClient.get<Employee[]>(url).pipe(
      catchError(err => this.handleError('get employees', err))
    )
  }

  private handleError(method: string, err: any) {
    console.log(`Error while trying to ${method}. ${err}`);
    return throwError(err);
  }  
}
