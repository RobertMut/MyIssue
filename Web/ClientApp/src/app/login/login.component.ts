import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router, NavigationStart } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  constructor(private router: Router) {}
  pass: string;
  login: string;
  onButton() {
    if (this.pass == "1234" && this.login == "admin") {
      this.router.navigate(['/home']);
    }
  }
}
