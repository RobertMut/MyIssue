import { Component } from '@angular/core';
import { AuthService } from "../../../services/AuthService";
import { Router, ActivatedRoute } from '@angular/router';
import { TaskService } from "../../../services/TaskService";
import { HttpErrorResponse } from '@angular/common/http';
import { taskroot } from "../../../models/taskroot";
import { task } from "../../../models/task";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  providers: [AuthService]
})
export class HomeComponent {
  public freetasks: taskroot;
  public activetasks: taskroot;
  public login: string;
  public selectionFree: number;
  public selectionActive: number;

  constructor(private auth: AuthService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private task: TaskService) {
    this.login = localStorage.getItem("login");
    this.selectionFree = Number.parseInt(localStorage.getItem("selectionfree"));
    this.selectionActive = Number.parseInt(localStorage.getItem("selectionactive"));
    if (isNaN(this.selectionFree)) {
      this.selectionFree = 5;
      localStorage.setItem("selectionfree", this.selectionFree.toString());
    }
    if (isNaN(this.selectionActive)) {
      this.selectionActive = 5;
      localStorage.setItem("selectionactive", this.selectionActive.toString());
    }
    task.getLastTasks(this.selectionFree, 'anybody', this.auth.headers()).subscribe(result => {
        this.freetasks = JSON.parse(result);
      },
      error => {
        console.error(error);
        this.auth.CheckUnauthorized(error);
      }
    );
    task.getLastTasks(this.selectionFree, localStorage.getItem("login"), this.auth.headers()).subscribe(result => {
        this.activetasks = JSON.parse(result);
      },
      error => {
        console.error(error);
        this.auth.CheckUnauthorized(error);
      }
    );

  }
  selectFreeChangeHandler(event: any) {
    this.selectionFree = event.target.value;
    localStorage.setItem("selectionfree", event.target.value.toString());
    location.reload();
  }
  selectActiveChangeHandler(event: any) {
    this.selectionFree = event.target.value;
    localStorage.setItem("selectionactive", event.target.value.toString());
    location.reload();
  }
  public selectTask(task: task) {
    console.warn("CLICKED!");
    this.router.navigate(['./task-view', task.taskId], {relativeTo: this.activatedRoute});
  }
}
