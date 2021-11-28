import { Component, OnInit } from '@angular/core';
import { UserTypeService } from "../../../services/UserTypeService";
import { PositionService } from "../../../services/PositionService";
import { AuthService } from "../../../services/AuthService";
import { User, UserRoot } from "../../../models/User";
import { UserService } from "../../../services/UserService";
import { EmployeeService } from "../../../services/EmployeeService";
import { Employee, EmployeeRoot } from "../../../models/Employee";
import { UserType } from "../../../models/UserType";
import { EmployeePosition } from "../../../models/EmployeePosition";
import { map } from 'rxjs';

@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html',
  styleUrls: ['./create-user.component.css'],
  providers: [UserTypeService, PositionService, UserService, EmployeeService]
})
export class CreateUserComponent implements OnInit {

  public userTypes: UserType[];
  public positions: EmployeePosition[];
  public users: User[];
  public employees: Employee[];

  constructor(private userTypeService: UserTypeService,
    private auth: AuthService,
    private positionService: PositionService,
    private userService: UserService,
    private employeeService: EmployeeService) {
    this.userService.getallusers(this.auth.headers()).subscribe(result => {
      let usersRoot: UserRoot = JSON.parse(result);
      this.users = usersRoot.users;
    },
      error => this.auth.CheckUnauthorized(error));
    this.employeeService.getAllEmployees(this.auth.headers()).subscribe(
      {
        next: (v) => this.employees = (v as EmployeeRoot).employees,
        error: (e) => {
          console.error(e);
          this.auth.CheckUnauthorized(e);
        }
      }
    );
    this.positionService.getEmployeePositions(this.auth.headers()).subscribe(result => {
        this.positions = JSON.parse(result);
    },
      error => this.auth.CheckUnauthorized(error));
    this.userTypeService.getUserTypes(this.auth.headers()).subscribe(result => {
        this.userTypes = JSON.parse(result);
      
    },
      error => this.auth.CheckUnauthorized(error));
  }

  ngOnInit(): void {
  }

  public createUser(userFormValues: any) {
    let user: User = {
      username: userFormValues.username,
      password: userFormValues.password,
      type: userFormValues.type
    }
    this.userService.createUser(user, this.auth.headers()).subscribe(error => console.error(error));
  }
  public createEmployee(employeeFormValues: any) {
    let employee: Employee = {
      login: employeeFormValues.login,
      name: employeeFormValues.name,
      surname: employeeFormValues.surname,
      no: employeeFormValues.no,
      position: employeeFormValues.position
    }
    this.employeeService.createEmployee(employee, this.auth.headers()).subscribe(error => console.error(error));
  }
}
