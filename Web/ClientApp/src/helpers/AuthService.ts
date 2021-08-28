import { Injectable } from '@angular/core';

@Injectable()
export class AuthService {

  public setEmptyStorage() {
    window.localStorage['login'] = null;
    window.localStorage['isLogged'] = false;
    window.localStorage['loginDate'] = null;
  }

  public isAuthenticated(): boolean {
    if (window.localStorage['isLogged'] == "false") {
      return false;
    }
    return true;
  }

  public logout() {
    window.localStorage['isLogged'] = false;
    window.localStorage['loginDate'] = null;
  }

  public login(username: string) {
    window.localStorage['login'] = username;
    window.localStorage['isLogged'] = true;
    window.localStorage['loginDate'] = Date.now();
  }

}
