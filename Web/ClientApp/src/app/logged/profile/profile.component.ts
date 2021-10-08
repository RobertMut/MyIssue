import { Component, OnInit } from '@angular/core';
import { AuthService } from "../../../services/AuthService";
import { Router, ActivatedRoute } from "@angular/router";
import { EmployeeService } from "../../../services/EmployeeService";
import { IEmployeeRoot, IEmployee } from "../../../interfaces/Employee";
import { UserService } from "../../../services/UserService";
import { IUser, IUserRoot } from "../../../interfaces/User";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css'],
  providers: [UserService, EmployeeService]
})
export class ProfileComponent implements OnInit {
  public user: IUser;
  public employee: IEmployee;
  public changed: string;
  constructor(private auth: AuthService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private userService: UserService,
    private employeeService: EmployeeService) {

    this.employeeService.getEmployeeByName(localStorage.getItem("login"), this.auth.headers()).subscribe(result => {
      let root: IEmployeeRoot = JSON.parse(result);
      this.employee = root.employees[0];
    },
    error => {
      console.error(error);
      this.auth.CheckUnauthorized(error);
    });
    this.userService.getuserbyname(localStorage.getItem("login"), this.auth.headers()).subscribe(result => {
      let root: IUserRoot = JSON.parse(result);
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
