import { Component, OnInit } from '@angular/core';
import { ITask, IPagedResponse } from "../../../interfaces/Task";
import { Router, ActivatedRoute } from '@angular/router';

import { TaskService } from "../../../services/TaskService";
import { AuthService } from "../../../services/AuthService";
import { EmployeeService } from "../../../services/EmployeeService";
import { ClientService } from "../../../services/TaskTypeService";
import { TaskTypeService } from "../../../services/ClientService";
import { IEmployeeRoot } from "../../../interfaces/Employee";
import { ITaskTypeRoot } from "../../../interfaces/TaskType";
import { IClientNameRoot } from "../../../interfaces/ClientName";

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

  constructor(private activeRoute: ActivatedRoute,
    private router: Router,
    private taskService: TaskService,
    private auth: AuthService,
    private employeeService: EmployeeService,
    private clientService: ClientService,
    private tasktypeService: TaskTypeService) {
    this.employeeService.getAllEmployees(this.auth.headers()).subscribe(result => {
        this.employees = JSON.parse(result);
        console.warn(this.employees.employees[0]);
      },
      error => {
        console.error(error);
        this.auth.CheckUnauthorized(error);
      });
    this.clientService.getClients(this.auth.headers()).subscribe(result => {
        this.clients = JSON.parse(result);
      },
      error => {
        console.error(error);
        this.auth.CheckUnauthorized(error);
      });
    this.tasktypeService.getTaskTypes(this.auth.headers()).subscribe(result => {
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
    if (name == 'removeStart')
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
    this.taskService.createTask(this.task, this.auth.headers()).subscribe(result => console.log(result.toString()));
    this.router.navigate(['./nav-menu-logged/home'], { relativeTo: this.activeRoute });
  }

}
