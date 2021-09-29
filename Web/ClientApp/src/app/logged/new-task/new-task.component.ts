import { Component, OnInit } from '@angular/core';
import { task, taskroot  } from "../../../models/task";
import { employeeroot, employee } from "../../../models/employee";
import { ActivatedRoute } from "@angular/router/router";
import { TaskService } from "../../../services/TaskService";
import { AuthService } from "../../../services/AuthService";
import { EmployeeService } from "../../../services/EmployeeService";

@Component({
  selector: 'app-new-task',
  templateUrl: './new-task.component.html',
  styleUrls: ['./new-task.component.css']
})
export class NewTaskComponent implements OnInit {
  public task: task;
  public assignment: string;
  public ownership: string;
  public employees: employeeroot;
  public createdByMail: boolean;

  constructor(private route: ActivatedRoute,
    private taskService: TaskService,
    private auth: AuthService,
    private employeeservice: EmployeeService) {
  }

  ngOnInit() {

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
  selectHandler(event: any): void {
    if (event.srcElement.name == "assign") this.task.taskAssignment = this.login(event);
    else this.task.taskOwner = this.login(event);
  }
  onStartButton(): void {
    this.task.taskStart = new Date().toISOString();
  }
  onEndButton(): void {
    this.task.taskEnd = new Date().toISOString();
  }
  sendtask(): void {
    this.taskService.updateTask(this.task, this.auth.headers()).subscribe(result => console.log(result.toString()));
      
  }
  private login(event): string {
    if (event.target.value.toString() == "null") return null;
    return this.employees.employees.find(k => k.login == event.target.value.toString()).login;
  }
  private checkMail() {
    if (this.task.createdByMail.length == 0) this.createdByMail = false;
    else this.createdByMail = true;
  }

}
