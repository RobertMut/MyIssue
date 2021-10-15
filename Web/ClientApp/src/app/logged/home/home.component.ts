import { Component } from '@angular/core';
import { AuthService } from "../../../services/AuthService";
import { Router, ActivatedRoute } from '@angular/router';
import { TaskService } from "../../../services/TaskService";
import { HttpErrorResponse } from '@angular/common/http';
import { Task, PagedResponse, TaskRoot } from "../../../interfaces/Task";

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
    this.selectionFree = event.target.value;
    localStorage.setItem("selectionFree", event.target.value.toString());
    location.reload();
  }
  selectActiveChangeHandler(event: any) {
    this.selectionFree = event.target.value;
    localStorage.setItem("selectionActive", event.target.value.toString());
    location.reload();
  }
  public navigateNewTask() {
    this.router.navigate(['./new-task'], { relativeTo: this.activatedRoute });
  }
  public navigatePaged() {
    this.router.navigate(['./task-paged'], { relativeTo: this.activatedRoute });
  }
  public navigateCreateClient() {
    this.router.navigate(['./create-client'], { relativeTo: this.activatedRoute });
  }
  public selectTask(task: Task) {
    this.router.navigate(['./nav-menu-logged/task-view', task.taskId], {relativeTo: this.activatedRoute});
  }
}
