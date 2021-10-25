import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Employee } from "../models/Employee";


@Injectable()
export class EmployeeService {
  private baseUrl: string;

  constructor(private httpclient: HttpClient,
    @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  public getAllEmployees(headers: HttpHeaders): Observable<string> {
    return this.httpclient
      .get(this.baseUrl + 'employees', { headers: headers, responseType: 'text' });
  }

  public getEmployeeByName(login: string, headers: HttpHeaders): Observable<string> {
    return this.httpclient
      .get(this.baseUrl + 'employees/' + login, { headers: headers, responseType: 'text' });
  }
  public createEmployee(employee: Employee, headers: HttpHeaders): Observable<string> {
    return this.httpclient
      .post(this.baseUrl + 'employees/', JSON.stringify(employee), { headers: headers, responseType: 'text' });
  }
}
