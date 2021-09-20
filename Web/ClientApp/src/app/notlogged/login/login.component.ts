import { Component, Inject } from '@angular/core';
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
    this.auth.login(this.loginForm.get(['login']).value, this.loginForm.get(['pass']).value)
      .subscribe((value: boolean) => {
        if (value == true) {
          this.router.navigate(['./../../nav-menu-logged']);
        } else {
         console.error("INCORRECT"); 
        }
      })
  }
}
