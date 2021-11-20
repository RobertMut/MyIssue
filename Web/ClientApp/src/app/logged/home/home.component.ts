import { Component } from '@angular/core';
import { AuthService } from "../../../services/AuthService";
import { Router, ActivatedRoute } from '@angular/router';
import { TaskService } from "../../../services/TaskService";
import { HttpErrorResponse } from '@angular/common/http';
import { Task, TaskRoot } from "../../../models/Task";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  providers: [AuthService]
})
export class HomeComponent {
  public freeTasks: Task[];
  public activeTasks: Task[];
  public login: string;
  public selectionFree: number;
  public selectionActive: number;

  constructor(private auth: AuthService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private task: TaskService) {
    this.login = localStorage.getItem("login");
    this.selectionFree = Number.parseInt(localStorage.getItem("selectionFree"));
    this.selectionActive = Number.parseInt(localStorage.getItem("selectionActive"));
    if (isNaN(this.selectionFree)) {
      this.selectionFree = 5;
      localStorage.setItem("selectionFree", this.selectionFree.toString());
    }
    if (isNaN(this.selectionActive)) {
      this.selectionActive = 5;
      localStorage.setItem("selectionActive", this.selectionActive.toString());
    }
    task.getLastTasks(this.selectionFree, 'anybody', this.auth.headers()).subscribe(result => {
        let data: TaskRoot = JSON.parse(result);
        this.freeTasks = data.tasks;

    },
      error => {
        console.error(error);
        this.auth.CheckUnauthorized(error);
      }
    );
    task.getLastTasks(this.selectionFree, localStorage.getItem("login").toString(), this.auth.headers()).subscribe(result => {
      let data: TaskRoot = JSON.parse(result);
      this.activeTasks = data.tasks;
    },
      error => {
        console.error(error);
        this.auth.CheckUnauthorized(error);
      }
    );

  }
  selectFreeChangeHandler(event: any) {
    this.selectionFree = event.value;
    localStorage.setItem("selectionFree", event.value.toString());
    location.reload();
  }
  selectActiveChangeHandler(event: any) {
    this.selectionFree = event.value;
    localStorage.setItem("selectionActive", event.value.toString());
    location.reload();
  }
  public navigate(link: string) {
    this.router.navigate([link], { relativeTo: this.activatedRoute });
  }
  public selectTask(taskId: number) {
    this.router.navigate(['./task-view', taskId.toString()], {relativeTo: this.activatedRoute});
  }
}
