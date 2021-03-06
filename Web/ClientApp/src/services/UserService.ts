import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from "../models/User";


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
  public changePassword(oldPass: string, newPass: string, name: string, headers: HttpHeaders): Observable<string> {
    let data: any = {
      oldPassword: oldPass,
      newPassword: newPass
    }
    return this.httpclient
      .post(this.baseUrl + 'Users/' + name, JSON.stringify(data), { headers: headers, responseType: 'text' });
  }
  public createUser(user: User, headers: HttpHeaders): Observable<string> {
    return this.httpclient
      .post(this.baseUrl + 'Users/new', JSON.stringify(user), { headers: headers, responseType: 'text' });
  }
}
