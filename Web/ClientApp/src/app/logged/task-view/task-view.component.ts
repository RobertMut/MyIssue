import { Component, OnInit, ViewChild, NgModule } from '@angular/core';
import { ActivatedRoute } from "@angular/router";
import { TaskService } from "../../../services/TaskService";
import { AuthService } from "../../../services/AuthService";
import { EmployeeService } from "../../../services/EmployeeService";
import { Task, TaskRoot } from "../../../models/Task";
import { EmployeeRoot } from "../../../models/Employee";
import { map } from 'rxjs';


@Component({
  selector: 'app-task-view',
  templateUrl: './task-view.component.html',
  styleUrls: ['./task-view.component.css'],
  providers: [AuthService, EmployeeService]
})
export class TaskViewComponent implements OnInit {
  public task: Task;
  public assignment: string;
  public ownership: string;
  public employees: EmployeeRoot;
  public createdByMail: boolean;

  constructor(private route: ActivatedRoute,
    private taskService: TaskService,
    private auth: AuthService,
    private employeeservice: EmployeeService) {
  }

  ngOnInit() {
    let id: number = Number.parseInt(this.route.snapshot.paramMap.get('id'));

    this.taskService.getTaskById(id, this.auth.headers()).subscribe(result => {
        let taskRoot: TaskRoot = JSON.parse(result);
        this.task = taskRoot.tasks[0];
        this.checkMail();
      },
      error => {
        console.error(error);
        this.auth.CheckUnauthorized(error);
      });

    this.employeeservice.getAllEmployees(this.auth.headers()).subscribe(
      {
        next: (v) => this.employees= (v as EmployeeRoot),
        error: (e) => {
          console.error(e);
          this.auth.CheckUnauthorized(e);
        }
      }
    );
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
  updateTask(): void {
    this.taskService.updateTask(this.task, this.auth.headers());
  }
  private checkMail() {
    if (this.task.CreatedByMail.length == 0) this.createdByMail = false;
    else this.createdByMail = true;
  }

}
