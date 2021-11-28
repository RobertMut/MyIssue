import { Component, OnInit } from '@angular/core';
import { AuthService } from "../../../services/AuthService";
import { Router, ActivatedRoute } from "@angular/router";
import { EmployeeService } from "../../../services/EmployeeService";
import { UserService } from "../../../services/UserService";
import { User, UserRoot } from "../../../models/User";
import { Employee, EmployeeRoot } from "../../../models/Employee";
import { map } from 'rxjs';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css'],
  providers: [UserService, EmployeeService]
})
export class ProfileComponent implements OnInit {
  public user: User;
  public employee: Employee;
  public changed: string;
  constructor(private auth: AuthService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private userService: UserService,
    private employeeService: EmployeeService) {

    this.employeeService.getEmployeeByName(localStorage.getItem("login"), this.auth.headers()).subscribe(
      {
        next: (v) => this.employee = (v as EmployeeRoot).employees[0],
        error: (e) => {
          this.auth.CheckUnauthorized(e);
        }
      }
    );
    this.userService.getuserbyname(localStorage.getItem("login"), this.auth.headers()).subscribe(result => {
      let root: UserRoot = JSON.parse(result);
      this.user = root.users[0];
    },
    error => {
      console.error(error);
      this.auth.CheckUnauthorized(error);
    });
  }

  ngOnInit(): void {
  }
  public changePassword(oldPass: any, newPass: any, newPassRepeated: any) {
    console.warn(newPass == newPassRepeated);
    if (newPass.toString() == newPassRepeated.toString()) {
      this.userService.changePassword(oldPass.toString(), newPass.toString(), this.user.username,
        this.auth.headers()).subscribe(result => {
          this.changed = result.toString();
        },
        error => {
          console.error(error);
          this.auth.CheckUnauthorized(error);
        });
    } else this.changed = 'New password does not match';

  }
}
