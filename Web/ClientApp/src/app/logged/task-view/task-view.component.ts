import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";
import { TaskService } from "../../../services/TaskService";
import { AuthService } from "../../../services/AuthService";
import { usernameroot } from "../../../models/usernameroot";
import { username } from "../../../models/username";
import { UserService } from "../../../services/UserService";
import { task } from "../../../models/task";
import { taskroot } from "../../../models/taskroot";

@Component({
  selector: 'app-task-view',
  templateUrl: './task-view.component.html',
  styleUrls: ['./task-view.component.css'],
  providers: [AuthService, UserService]
})
export class TaskViewComponent implements OnInit {
  public task: task;
  public users: usernameroot;

  constructor(private route: ActivatedRoute,
    private taskService: TaskService,
    private auth: AuthService,
    private userservice: UserService) {

    let id: number = Number.parseInt(this.route.snapshot.paramMap.get('id'));

    this.taskService.getTaskById(id, this.auth.headers()).subscribe(result => {
      let taskroot: taskroot = JSON.parse(result);
      this.task = taskroot.tasks[0];
    },
      error => {
        console.error(error);
        this.auth.CheckUnauthorized(error);
      });

    this.userservice.getallusers(this.auth.headers()).subscribe(result => {
      this.users = JSON.parse(result);
      
    },
      error => {
        console.error(error);
        this.auth.CheckUnauthorized(error);
      });
  }
  selectAssignmentHandler(event: any) {
    this.task.taskAssignment = this.users.users.find(k => k.username == event.target.value.toString()).username;
    console.warn(this.task.taskAssignment);
  }
  selectOwnerHandler(event: any) {
    this.task.taskOwner = this.users.users.find(k => k.username == event.target.value.toString()).username;
    console.warn(this.task.taskOwner);
  }

  ngOnInit() {
  }

}
