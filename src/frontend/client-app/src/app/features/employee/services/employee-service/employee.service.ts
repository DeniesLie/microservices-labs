import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { Employee, EmployeeCreate, EmployeeUpdate } from 'src/app/core/entities/employee';
import { catchError, Observable, Subject, tap, throwError } from 'rxjs';
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
  baseUrl = `${environment.apiUrl}/employeeservice/api/employees`;
  changed$ : Subject<any> = new Subject<any>();

  constructor(
    private _httpClient: HttpClient
  ) { }

  getByStorageId(storageId: string): Observable<Employee[]> {
    const url = `${this.baseUrl}/GetByStorage/${storageId}`; 
    return this._httpClient.get<Employee[]>(url).pipe(
      catchError(err => this.handleError('get employees', err))
    )
  }

  create(employee: EmployeeCreate): Observable<Employee> {
    return this._httpClient.post<Employee>(this.baseUrl, employee, this.httpOptions)
      .pipe(
        tap(employee => this.changed$.next(employee)),
        catchError(err => this.handleError('create employee', err))
      )
  }

  update(employee: EmployeeUpdate): Observable<Employee> {
    return this._httpClient.put<Employee>(this.baseUrl, employee, this.httpOptions).pipe(
      tap(employee => this.changed$.next(employee)),
      catchError(err => this.handleError('update employee', err)))
  }

  delete(employeeId: string) {
    const url = `${this.baseUrl}/${employeeId}`;
    return this._httpClient.delete(url).pipe(
      tap(obj => this.changed$.next(obj)),
      catchError(err => this.handleError('delete employee', err))
    );
  }
  
  private handleError(method: string, err: any) {
    console.log(`Error while trying to ${method}. ${err}`);
    return throwError(err);
  }  
}
