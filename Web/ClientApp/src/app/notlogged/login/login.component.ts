import { Component } from '@angular/core';
import { Router, ActivatedRoute } from "@angular/router";
import { AuthService } from "../../../services/AuthService";
import { FormBuilder } from '@angular/forms';
import { TaskService } from "../../../services/TaskService";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  providers: [TaskService]
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
    this.auth.login();
  }
}
