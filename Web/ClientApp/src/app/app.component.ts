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
  constructor(public router: Router, public helper: AuthService) {
    if (localStorage.getItem('login') == null &&
      localStorage.getItem('isLogged') == null &&
      localStorage.getItem('loginDate') == null)
      console.warn('test');
      //this.helper.setEmptyStorage();
  }
  ngOnInit() {
    this.router.events
      .subscribe((e) => {
          if ((e instanceof NavigationEnd && !this.helper.isAuthenticated())) {
            this.router.navigate(['login']);
          } 
        }
      );
  }

}
