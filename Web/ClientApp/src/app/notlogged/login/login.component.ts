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

  pass: string;
  login: string;
  onButton() {
    if (this.pass == "1234" && this.login == "admin") {
      this.helpers.login('admin');
      this.router.navigate(['../nav-menu-logged/home']);
    }
  }
}
