import { Component, Inject } from '@angular/core';
import { Router, ActivatedRoute } from "@angular/router";
import { AuthService } from "../../../services/AuthService";
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  constructor(private router: Router,
    private auth: AuthService,
    private form: FormBuilder,
    private active: ActivatedRoute) {
  }
  public loginForm = this.form.group({
    login: '',
    pass: ''
  })
  onButton(): void {
    if (this.auth.login(this.loginForm.get(['login']).value, this.loginForm.get(['pass']).value)) {
      this.router.navigate(['../nav-menu-logged/home'], {relativeTo: this.active});
    }
  }
}
