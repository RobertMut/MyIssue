import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { map, catchError } from "rxjs/operators";
import { Observable } from 'rxjs';

const headers: HttpHeaders = new HttpHeaders
  ({
    'Content-Type': 'application/json',
    'Accept': '*/*'
  });

@Injectable()
export class AuthService {
  baseUrl: string;
  private isValidToken: boolean;
  constructor(private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  public logout() {
    let token = localStorage.getItem("token");
    let data = {
      "TokenString": token
    };
    this.http.post(this.baseUrl + "Auth/logout",
      JSON.stringify(data),
      {
        headers: headers,
        responseType: 'text'
      }).pipe(
      map(response => {
        var obj = JSON.parse(response.toString());
        localStorage.setItem("type", "");
        localStorage.setItem("token", obj.toString());

      })
    );

  }
  public headers(): HttpHeaders {
    let headers = new HttpHeaders();
    headers.append('Accept', '*/*');
    headers.append('Content-Type', 'application/json');
    headers.append('Authorization', 'User ' + localStorage.getItem("token"));
    return headers;
  }

  public tokenlogin(): Observable<boolean> {

    let data = {
      "Login": localStorage.getItem("login"),
      "Token": localStorage.getItem("token")
    }
    let object = JSON.stringify(data);
    return this.http.post(this.baseUrl + "Auth/tokenlogin",
      object,
      {
        headers: headers,
        responseType: 'text' as 'text'
      }).pipe(
        map(response => {
          try {
            return JSON.parse(response.toString()).result == "true";
          } catch (e) {
            return false;
          }

        })
      );
  }

  public login(login: string, pass: string): Observable<boolean> {
    let data = {
      "Login": login,
      "Password": pass
    }
    console.warn(data);
    console.warn(JSON.stringify(data));
    console.warn(this.baseUrl + "Auth/login");
    return this.http.post(this.baseUrl + "Auth/login",
      JSON.stringify(data),
      {
        headers: headers,
        responseType: 'text' as 'text'
      }).pipe(
        map(response => {
          try {
            let obj = JSON.parse(response.toString());
            localStorage.setItem("login", obj.login);
            localStorage.setItem("token", obj.token);
            localStorage.setItem("type", obj.type.toString());
            return true;
          } catch (e) {
            return false;
          }

        })
      );
  }
}
