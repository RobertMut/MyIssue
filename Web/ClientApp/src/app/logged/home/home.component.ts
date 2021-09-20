import { Component } from '@angular/core';
import { AuthService } from "../../../services/AuthService";
import { Router, ActivatedRoute } from '@angular/router';
import { TaskService } from "../../../services/TaskService";
import { HttpErrorResponse } from '@angular/common/http';

import { ITask } from "../../../models/task.model";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  providers: [TaskService]
})
export class HomeComponent {
  public tasks: ITask[];
  public login: string;
  constructor(private auth: AuthService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private task: TaskService) {
    this.login = localStorage.getItem("login");
    task.getLastTasks(2).subscribe(result => {
        this.tasks = result;
      },
      error => {
        console.error(error);
        const httpEx = error as HttpErrorResponse;
        if (httpEx.status == 401 || httpEx.status == 403) {
          //this.auth.logout();
          //this.router.navigate(['../nav-menu']);
        }
      }
    );

    console.warn(this.tasks);
  }

}
