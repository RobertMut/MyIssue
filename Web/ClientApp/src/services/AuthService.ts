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
  constructor(private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  public logout(): void{
    let token = localStorage.getItem("token");
    let data = {
      "TokenString": token
    };
    console.warn("logout..");
    this.http.post(this.baseUrl + "Auth/logout",
      JSON.stringify(data),
      {
        headers: headers,
        responseType: 'text' as 'text'
      }).subscribe(response => {
      localStorage.setItem("type", "");
      localStorage.setItem("token", response);
    });
    

  }
  public headers(): HttpHeaders {
    return new HttpHeaders({
      'Accept': '*/*',
      'Content-Type': 'application/json',
      'Authorization': localStorage.getItem('login') + ' ' + localStorage.getItem("token")
    });
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
            let json = JSON.parse(response.toString()).result
            return (/true/i).test(json);
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
