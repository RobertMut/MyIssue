import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable()
export class EmployeeService {
  private baseUrl: string;

  constructor(private httpclient: HttpClient,
    @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  public getallemployees(headers: HttpHeaders): Observable<string> {
    return this.httpclient
      .get(this.baseUrl + 'employees', { headers: headers, responseType: 'text' });
  }

  public getemployeebyname(login: string, headers: HttpHeaders): Observable<string> {
    return this.httpclient
      .get(this.baseUrl + 'employees/' + login, { headers: headers, responseType: 'text' });
  }
}
