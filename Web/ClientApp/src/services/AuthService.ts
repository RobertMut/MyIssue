import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders, HttpHeaderResponse } from '@angular/common/http';
//import { Authenticate } from '../models/authenticate.model';

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

  public logout() {
    let token = localStorage.getItem("token");
    let data = {
      "TokenString": token
    };
    let header = this.headers();
    this.http.post(this.baseUrl + "Auth/logout",
      JSON.stringify(data),
      {
        headers: header,
        responseType: 'text'
      }).subscribe(Response => {
      var obj = JSON.parse(Response);
        localStorage.setItem("type", "");
        localStorage.setItem("token", Response.toString());
      });

  }
  public headers(): HttpHeaders {
    let headers = new HttpHeaders();
    headers.append('Accept', '*/*');
    headers.append('Content-Type', 'application/json');
    headers.append('Authorization', 'User ' + localStorage.getItem("token"));
    return headers;
  }

  public tokenlogin(): boolean {
    let data = {
      "Login": localStorage.getItem("login"),
      "Token": localStorage.getItem("token")
    }
    let bool: boolean
    this.http.post(this.baseUrl + "Auth/tokenlogin", JSON.stringify(data),
      {
        headers: headers,
        responseType: 'text'
      }).subscribe(Response => {
      var obj = JSON.parse(Response);
        if (obj.result == 'true') bool = true;
        else bool = false;
      }, Error => {
        console.error(Error);
        bool = false;
      });
    return bool;
  }

  public login(login: string, pass: string): boolean {
    let data = {
      "Login": login,
      "Password": pass
    }
    let bool: boolean;
    console.warn(data);
    console.warn(JSON.stringify(data));
    console.warn(this.baseUrl + "Auth/login");
    this.http.post(this.baseUrl + "Auth/login", JSON.stringify(data),
      {
        headers: headers,
        responseType: 'text'
      }).subscribe(Response => {
        if (!(Response == null)) {
          var obj = JSON.parse(Response);
          localStorage.setItem("login", obj.login);
          localStorage.setItem("token", obj.token);
          localStorage.setItem("type", obj.type.toString());
          bool = true;

        } else bool = false;
      },
        Error => {
          console.error(Error);
          bool = false;
        });
    return bool;
  }
}
