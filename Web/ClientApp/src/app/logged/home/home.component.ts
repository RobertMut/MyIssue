import { Component } from '@angular/core';
import { AuthService } from "../../../services/AuthService";
import { Router, ActivatedRoute } from '@angular/router';
import { TaskService } from "../../../services/TaskService";

import { ITask } from "../../../models/task.model";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  public tasks: ITask[];

  constructor(private auth: AuthService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private task: TaskService) {
    task.getLastTasks(2).subscribe(result => {
        this.tasks = result;
      },
      error => console.error(error)
    );
    console.warn(this.tasks);
  }

  ngOnInit() {
    if (!this.auth.tokenlogin()) {
      this.router.navigate(['./logout'], {relativeTo: this.activatedRoute});
    }

  }
}
