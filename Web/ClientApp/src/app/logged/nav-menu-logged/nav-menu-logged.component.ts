import { Component, OnInit } from '@angular/core';
import { OidcSecurityService, OidcClientNotification, OpenIdConfiguration, UserDataResult } from 'angular-auth-oidc-client';
import { Observable } from 'rxjs';
@Component({
  selector: 'app-nav-menu-logged',
  templateUrl: './nav-menu-logged.component.html',
  styleUrls: ['./nav-menu-logged.component.css']
})
export class NavMenuLoggedComponent implements OnInit {
  public login: string;

  configuration: OpenIdConfiguration;
  userDataChanged$: Observable<OidcClientNotification<any>>;
  userData$: Observable<UserDataResult>;
  isAuthenticated = false;
  constructor(public oidcSecurityService: OidcSecurityService) {
    this.login = localStorage.getItem("login");
  }

  ngOnInit() {
    
  }

}
