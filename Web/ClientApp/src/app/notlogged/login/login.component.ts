import { Component, Inject } from '@angular/core';
import { Router, NavigationStart } from '@angular/router';
import { AuthService } from "../../../helpers/AuthService";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  constructor(private router: Router, private helpers: AuthService) { }
  ngOnInit() {
    console.warn("navigation to home");
    if (this.helpers.isAuthenticated()) {
      
      this.router.navigate(['home']);
    }
  }
  pass: string;
  login: string;
  onButton() {
    if (this.pass == "1234" && this.login == "admin") {
      this.helpers.login('admin');
      this.router.navigate(['home']);
    }
  }
}
