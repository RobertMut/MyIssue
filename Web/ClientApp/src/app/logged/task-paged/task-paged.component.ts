import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";
import { TaskService } from "../../../services/TaskService";
import { AuthService } from "../../../services/AuthService";
import { MatSelectChange } from "@angular/material/select"
import { ITask, IPagedResponse } from "../../../interfaces/Task";

@Component({
  selector: 'app-task-paged',
  templateUrl: './task-paged.component.html',
  styleUrls: ['./task-paged.component.css']
})
export class TaskPagedComponent implements OnInit {
  public paged: IPagedResponse = {} as IPagedResponse;
  constructor(private activeRoute: ActivatedRoute,
    private router: Router,
    private taskService: TaskService,
    private auth: AuthService) {
    if (localStorage.getItem("pageSize") == null) localStorage.setItem("pageSize", "10");
    let size: number = Number.parseInt(localStorage.getItem("pageSize"));
    this.taskService.getPagedFirst(1, size, this.auth.headers()).subscribe(result => {
      console.warn(result);
      this.paged = JSON.parse(result);
    },
    error => {
      console.error(error);
      this.auth.CheckUnauthorized(error);
    }
    );

  }

  ngOnInit(): void {

  }

  public selectTask(task: ITask) {
    this.router.navigate(['nav-menu-logged/task-view', task.taskId]);
  }
  public firstPageButton() {
    this.taskService.getPagedLink(this.paged.firstPage, this.auth.headers()).subscribe(result => {
      this.paged = JSON.parse(result);
    },
    error => {
      console.error(error);
      this.auth.CheckUnauthorized(error);
    });
  }
  public previousPageButton() {
    this.taskService.getPagedLink(this.paged.previousPage, this.auth.headers()).subscribe(result => {
      console.warn(result);
      this.paged = JSON.parse(result);
    },
    error => {
      console.error(error);
      this.auth.CheckUnauthorized(error);
    });
  }
  public nextPageButton() {
    this.taskService.getPagedLink(this.paged.nextPage, this.auth.headers()).subscribe(result => {
      console.warn(result);
      this.paged = JSON.parse(result);
    },
    error => {
      console.error(error);
      this.auth.CheckUnauthorized(error);
    });
  }
  public lastPageButton() {
    this.taskService.getPagedLink(this.paged.lastPage, this.auth.headers()).subscribe(result => {
      this.paged = JSON.parse(result);
    },
    error => {
      console.error(error);
      this.auth.CheckUnauthorized(error);
    });
  }
  public selectTaskPerPageHandler(event: MatSelectChange) {
    localStorage.setItem("pageSize", event.value);
  }
  public goTo(pageNumber: any, pageSize: any) {
    this.taskService.getPagedFirst(Number.parseInt(pageNumber.toString()),
      Number.parseInt(pageSize.toString()), this.auth.headers()).subscribe(result => {
      this.paged = JSON.parse(result);
    },
    error => {
      console.error(error);
      this.auth.CheckUnauthorized(error);
    });
  }
}
