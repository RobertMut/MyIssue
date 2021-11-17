import { Component } from '@angular/core';
import { AuthService } from "../services/AuthService";
import { Router, NavigationEnd } from '@angular/router';
import { OidcSecurityService } from 'angular-auth-oidc-client';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  providers: [AuthService]
})
export class AppComponent {
  title = 'MyIssue Web';
  constructor(public router: Router, public oidcSecurityService: OidcSecurityService) {

  }
  ngOnInit() {
    this.oidcSecurityService.checkAuthIncludingServer()
      .subscribe((isAuthenticated) => console.log('app authenticated', isAuthenticated));
  }

}
