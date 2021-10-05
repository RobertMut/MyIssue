import { Component, OnInit } from '@angular/core';
import { ITask, ITaskroot, IPagedTaskRequest, IPagedResponse } from "../../../models/task";
import { ActivatedRoute, Router } from "@angular/router";
import { TaskService } from "../../../services/TaskService";
import { AuthService } from "../../../services/AuthService";

@Component({
  selector: 'app-task-paged',
  templateUrl: './task-paged.component.html',
  styleUrls: ['./task-paged.component.css']
})
export class TaskPagedComponent implements OnInit {
  public paged: IPagedResponse = {} as IPagedResponse;
  constructor(private activeroute: ActivatedRoute,
    private router: Router,
    private taskService: TaskService,
    private auth: AuthService) {
    if (localStorage.getItem("pageSize") == null) localStorage.setItem("pageSize", "15");
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
    this.router.navigate(['./task-view', task.taskId], { relativeTo: this.activeroute});
  }
  public firstPageButton() {
    this.taskService.getPagedLink(this.paged.firstPage, this.auth.headers()).subscribe(result => {
      this.paged = JSON.parse(result);
    });
  }
  public previousPageButton() {
    this.taskService.getPagedLink(this.paged.previousPage, this.auth.headers()).subscribe(result => {
      console.warn(result);
      this.paged = JSON.parse(result);
    });
  }
  public nextPageButton() {
    this.taskService.getPagedLink(this.paged.nextPage, this.auth.headers()).subscribe(result => {
      console.warn(result);
      this.paged = JSON.parse(result);
    });
  }
  public lastPageButton() {
    this.taskService.getPagedLink(this.paged.lastPage, this.auth.headers()).subscribe(result => {
      this.paged = JSON.parse(result);
    });
  }

}
