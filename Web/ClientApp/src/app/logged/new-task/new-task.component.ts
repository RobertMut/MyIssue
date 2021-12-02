import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { first, pipe, map } from 'rxjs';
import { TaskService } from "../../../services/TaskService";
import { AuthService } from "../../../services/AuthService";
import { EmployeeService } from "../../../services/EmployeeService";
import { ClientService } from "../../../services/ClientService";
import { TaskTypeService } from "../../../services/TaskTypeService";
import { Task } from "../../../models/Task";
import { EmployeeRoot } from "../../../models/Employee";
import { TaskTypeRoot } from "../../../models/TaskType";
import { ClientRoot } from "../../../models/Client";

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
  public task: Task = {} as Task;
  public assignment: string;
  public ownership: string;
  public employees;
  public createdByMail: boolean;
  public taskTypes;
  public clients;

  constructor(private activeRoute: ActivatedRoute,
    private router: Router,
    private taskService: TaskService,
    private auth: AuthService,
    private employeeService: EmployeeService,
    private clientService: ClientService,
    private tasktypeService: TaskTypeService) {
    this.initialize();
  }
  ngOnInit() {
  }
  clearDate(name): void {
    if (name == 'removeStart')
      this.task.TaskStart = null;
    else this.task.TaskEnd = null;
  }
  onStartButton(): void {
    this.task.TaskStart = new Date().toISOString();
  }
  onEndButton(): void {
    this.task.TaskEnd = new Date().toISOString();
  }
  posttask(): void {
    this.taskService.createTask(this.task, this.auth.headers()).subscribe(result => console.log(result.toString()));
    this.router.navigate(['./nav-menu-logged/home'], { relativeTo: this.activeRoute });
  }
  private async initialize(){
    await this.employeeService.getAllEmployees(this.auth.headers()).subscribe(
      {
        next: (v) => this.employees = v as EmployeeRoot,
        error: (e) => {
          console.error(e);
          this.auth.CheckUnauthorized(e);
        }
      }
    );
  await this.clientService.getClients(this.auth.headers()).subscribe(
    {
      next: (v) => this.clients = v as ClientRoot,
      error: (e) => {
        console.error(e);
        this.auth.CheckUnauthorized(e);
      }
    }
  );
  await this.tasktypeService.getTaskTypes(this.auth.headers()).subscribe(
    {
      next: (v) => this.taskTypes = v as TaskTypeRoot,
      error: (e) => {
        console.error(e);
        this.auth.CheckUnauthorized(e);
      }
    }
  );
    this.task.TaskAssignment = localStorage.getItem("login");
    this.task.TaskOwner = localStorage.getItem("login");
  }
}
