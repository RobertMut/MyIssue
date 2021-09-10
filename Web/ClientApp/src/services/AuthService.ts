import { Injectable } from '@angular/core';
import { User } from '../classes/User'
import { HttpClient } from '@angular/common/http'
interface sessionStorage {
  login: string;
  usertype: string;
  token: string;
}
@Injectable()
export class AuthService {
  constructor (private http: HttpClient){}

//  public setEmptyStorage() {
//    window.localStorage['login'] = null;
//    window.localStorage['isLogged'] = false;
//    window.localStorage['loginDate'] = null;
//  }

  public isAuthenticated(): boolean {
    if (window.localStorage['isLogged'] == "false") {
      return false;
    }
    return true;
  }

  public logout() {
    sessionStorage.clear();
  }

  public login(login: string, pass: string, url: string) {
    let data = {
      "login": login,
      "pass": pass
    };
    let value = this.http.post(url + "logging/", data);
    let json: sessionStorage = JSON.parse(value.toString());
    if (!(json == null)) {
      sessionStorage.setItem("login", json.login);
      sessionStorage.setItem("usertype", json.usertype);
      sessionStorage.setItem("token", json.token);
    }
    
  }
}
