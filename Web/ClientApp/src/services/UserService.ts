import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable()
export class UserService {
  private baseUrl: string;

  constructor(private httpclient: HttpClient,
    @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  public getallusers(headers: HttpHeaders): Observable<string> {
    return this.httpclient
      .get(this.baseUrl + 'Users', { headers: headers, responseType: 'text' });
  }

  public getuserbyname(name: string, headers: HttpHeaders): Observable<string> {
    return this.httpclient
      .get(this.baseUrl + 'Users/' + name, { headers: headers, responseType: 'text' });
  }
}
