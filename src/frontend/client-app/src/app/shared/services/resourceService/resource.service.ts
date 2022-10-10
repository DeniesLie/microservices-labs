import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';


export abstract class ResourceService<T> {

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  constructor(
    protected httpClient: HttpClient
  ) { }
}
