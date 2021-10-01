import { Component, OnInit, ViewChild, NgModule } from '@angular/core';
import { ActivatedRoute } from "@angular/router";
import { TaskService } from "../../../services/TaskService";
import { AuthService } from "../../../services/AuthService";
import { EmployeeService } from "../../../services/EmployeeService";
import { ITask, ITaskroot } from "../../../models/task";
import { IEmployeeRoot } from "../../../models/employee";


@Component({
  selector: 'app-task-view',
  templateUrl: './task-view.component.html',
  styleUrls: ['./task-view.component.css'],
  providers: [AuthService, EmployeeService]
})
export class TaskViewComponent implements OnInit {
  public task: ITask;
  public assignment: string;
  public ownership: string;
  public employees: IEmployeeRoot;
  public createdByMail: boolean;

  constructor(private route: ActivatedRoute,
    private taskService: TaskService,
    private auth: AuthService,
    private employeeservice: EmployeeService) {
  }

  ngOnInit() {
    let id: number = Number.parseInt(this.route.snapshot.paramMap.get('id'));

    this.taskService.getTaskById(id, this.auth.headers()).subscribe(result => {
        let taskroot: ITaskroot = JSON.parse(result);
        this.task = taskroot.tasks[0];
        this.checkMail();
      },
      error => {
        console.error(error);
        this.auth.CheckUnauthorized(error);
      });

    this.employeeservice.getallemployees(this.auth.headers()).subscribe(result => {
      this.employees = JSON.parse(result);

    },
      error => {
        console.error(error);
        this.auth.CheckUnauthorized(error);
      });
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
  updatetask(): void {
    console.warn(JSON.stringify(this.task).toString());
    this.taskService.updateTask(this.task, this.auth.headers()).subscribe(result => console.log(result.toString()));
  }
  private checkMail() {
    if (this.task.createdByMail.length == 0) this.createdByMail = false;
    else this.createdByMail = true;
  }

}
