import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { map } from "rxjs/operators";
import { Observable } from 'rxjs';
import { Router, ActivatedRoute } from "@angular/router";
const headers: HttpHeaders = new HttpHeaders
  ({
    'Content-Type': 'application/json',
    'Accept': '*/*'
  });

@Injectable()
export class AuthService {
  baseUrl: string;
  constructor(private http: HttpClient,
  private router: Router,
  private activeRoute: ActivatedRoute,
    @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  public logout(): void{
    localStorage.setItem("type", "");
    localStorage.setItem("token", "");
    localStorage.setItem("login", "")
    console.warn("logout..");
    this.router.navigate(['/nav-menu/login'], { relativeTo: this.activeRoute })

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
      }).pipe(map(result => {
        if(result == 'Unauthorized') return false;
        else return true;
      }))
  }
  public login(login: string, pass: string): Observable<boolean> {
    let data = {
      "UserName": login,
      "Password": pass
    }
    //console.warn(data);
    //console.warn(JSON.stringify(data));
    //console.warn(this.baseUrl + "Auth/login");
    localStorage.clear();
    return this.http.post(this.baseUrl + "Auth/login",
      JSON.stringify(data),
      {
        headers: headers,
        responseType: 'text' as 'text'
      }).pipe(
      map(response => {
        try {
          localStorage.setItem("login", login);
          localStorage.setItem("token", response.toString());
          //localStorage.setItem("type", obj.type.toString());
          return true;
        } catch (e) {
          return false;
        }

      })
    );
  }

  public CheckUnauthorized(exception: any) {
    const httpEx = exception as HttpErrorResponse;
    if (httpEx.status == 401 || httpEx.status == 403) {
      this.logout();
    }
  }
}
