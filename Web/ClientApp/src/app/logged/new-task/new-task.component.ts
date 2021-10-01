import { Component, OnInit } from '@angular/core';
import { ITask, ITaskroot } from "../../../models/task";
import { IEmployeeRoot, IEmployee } from "../../../models/employee";
import { Router, ActivatedRoute } from '@angular/router';

import { TaskService } from "../../../services/TaskService";
import { AuthService } from "../../../services/AuthService";
import { EmployeeService } from "../../../services/EmployeeService";
import { IClientNameRoot, IClientName } from "../../../models/clientname";
import { ClientService } from "../../../services/TaskTypeService";
import { TaskTypeService } from "../../../services/ClientService";

enum Selector {
  taskType = "tasktypeSelect",
  client = "clientSelect",
  assignment = "assignmentSelect",
  ownership = "ownershipSelect"
}
@Component({
  selector: 'app-new-task',
  templateUrl: './new-task.component.html',
  styleUrls: ['./new-task.component.css'],
  providers: [AuthService, ClientService, TaskTypeService, TaskService, EmployeeService]
})

export class NewTaskComponent implements OnInit {
  public task: ITask = {} as ITask;
  public assignment: string;
  public ownership: string;
  public employees: IEmployeeRoot;
  public createdByMail: boolean;
  public taskTypes: ITaskTypeRoot;
  public clients: IClientNameRoot;

  constructor(private activeroute: ActivatedRoute,
    private router: Router,
    private taskService: TaskService,
    private auth: AuthService,
    private employeeservice: EmployeeService,
    private clientservice: ClientService,
    private tasktypeservice: TaskTypeService) {
    this.employeeservice.getallemployees(this.auth.headers()).subscribe(result => {
        this.employees = JSON.parse(result);
        console.warn(this.employees.employees[0]);
      },
      error => {
        console.error(error);
        this.auth.CheckUnauthorized(error);
      });
    this.clientservice.getClients(this.auth.headers()).subscribe(result => {
        this.clients = JSON.parse(result);
      },
      error => {
        console.error(error);
        this.auth.CheckUnauthorized(error);
      });
    this.tasktypeservice.getTaskTypes(this.auth.headers()).subscribe(result => {
        this.taskTypes = JSON.parse(result);
        console.warn(this.taskTypes.taskTypes[0]);
      },
      error => {
        console.error(error);
        this.auth.CheckUnauthorized(error);
      });
  }

  ngOnInit() {
    this.task.taskAssignment = localStorage.getItem("login");
    this.task.taskOwner = localStorage.getItem("login");
  }
  clearDate(name): void {
    if (name == 'removestart')
      this.task.taskStart = null;
    else this.task.taskEnd = null;
  }
  onStartButton(): void {
    this.task.taskStart = new Date().toISOString();
  }
  onEndButton(): void {
    this.task.taskEnd = new Date().toISOString();
  }
  posttask(): void {
    this.taskService.createTask(this.task, this.auth.headers()).subscribe(result => console.log(result.toString()));;
    this.router.navigate(['./nav-menu-logged/home'], { relativeTo: this.activeroute });
  }

}
