import { OnInit, Injectable, Inject } from '@angular/core';
import { OidcClientNotification, OidcSecurityService, OpenIdConfiguration, UserDataResult } from 'angular-auth-oidc-client';
import { Observable } from 'rxjs';


@Injectable()
export class AuthService implements OnInit {
  baseUrl: string;
  authUrl: string;

  configuration: OpenIdConfiguration;
  userDataChanged$: OidcClientNotification<any>;
  userData$: UserDataResult;
  isAuthenticated = false;
  checkSessionChanged$: Observable<boolean>;
  checkSessionChanged: any;

  constructor(public oidcSecurityService: OidcSecurityService,
    @Inject('BASE_URL') baseUrl: string,
    @Inject('AUTH_URL') authUrl: string) {
    this.baseUrl = baseUrl;
    this.authUrl = authUrl;
  }

  ngOnInit() {
    this.configuration = this.oidcSecurityService.getConfiguration();
    this.oidcSecurityService.userData$.subscribe(({ userData }) => {
      this.userData$ = userData;
    });
    this.checkSessionChanged$ = this.oidcSecurityService.checkSessionChanged$;

    this.oidcSecurityService.isAuthenticated$.subscribe(({ isAuthenticated }) => {
      this.isAuthenticated = isAuthenticated;
      console.warn('isAuthenticated: ', isAuthenticated);
    });

  }
  public login() {
    console.log('login start');
    this.oidcSecurityService.authorize();
  }
  public refreshSessionCheckSession() {
    console.log('refreshSession');
    this.oidcSecurityService.authorize();
  }
  public forceRefreshSession() {
    console.log('forced refreshSession');
    this.oidcSecurityService.forceRefreshSession().subscribe((result) => {
      console.log(result);
    });
  }
  public logout() {
    console.log('logout');
    this.oidcSecurityService.logoff();
  }

}
