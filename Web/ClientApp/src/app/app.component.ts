import { Component } from '@angular/core';
import { PageListenerService } from "../services/PageListenerService";
import { AuthService } from "../services/AuthService";
import { Router, NavigationEnd } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  providers: [AuthService]
})
export class AppComponent {
  title = 'MyIssue Web';
  constructor(public router: Router, public auth: AuthService) {

  }
  ngOnInit() {
    this.router.events
      .subscribe((e) => {
          if ((e instanceof NavigationEnd && !this.auth.tokenlogin())) {
            this.router.navigate(['login']);
          } 
        }
      );
  }

}
