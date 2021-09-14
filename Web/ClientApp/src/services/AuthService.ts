import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders, HttpHeaderResponse } from '@angular/common/http';
interface sessionStorage {
  login: string;
  type: string;
  token: string;
}
@Injectable()
export class AuthService {
  baseUrl: string;
  constructor(private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  public logout() {
    let token = sessionStorage.getItem("token");
    let data = {
      "TokenString": token
    };
    let header = this.headers();
    let value = this.http.post(this.baseUrl + "Auth/logout",
      data,
      {
        headers: header
      });
    sessionStorage.setItem("login", "");
    sessionStorage.setItem("type", "");
    sessionStorage.setItem("token", value.toString());
  }
  public headers(): HttpHeaders {
    let headers = new HttpHeaders();
    headers.append('Accept', '*/*');
    headers.append('Content-Type', 'application/json');
    headers.append('Authorization', 'User ' + sessionStorage.getItem("token"));
    return headers;
  }

  public tokenlogin(): boolean {
    let data = {
      "Login": sessionStorage.getItem("login"),
      "Token": sessionStorage.getItem("token")
    }
    let value = this.http.post(this.baseUrl + "Auth/tokenlogin", data);
    return (value.toString() == 'true');
  }

  public login(login: string, pass: string): boolean {
    let data = {
      "Login": login,
      "Password": pass
    };
    let value = this.http.post(this.baseUrl + "Auth/login", data);
    let json: sessionStorage = JSON.parse(value.toString());
    if (!(json == null)) {
      sessionStorage.setItem("login", json.login);
      sessionStorage.setItem("type", json.type);
      sessionStorage.setItem("token", json.token);
      return true;
    } else {
      return false;
    }

  }
}
