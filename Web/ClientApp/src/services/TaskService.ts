import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { task, taskroot } from "../models/task";


@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private baseUrl: string;

  constructor(private httpclient: HttpClient,
    @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  public getLastTasks(howMany: number, whose: string, headers: HttpHeaders): Observable<string> {
    return this.httpclient
      .get(this.baseUrl + 'Tasks/someOpen/' + whose + '/' + howMany, { headers: headers, responseType: 'text' });
  }

  public getTaskById(id: number, headers: HttpHeaders): Observable<string> {
    return this.httpclient
      .get(this.baseUrl + 'Tasks/' + id, { headers: headers, responseType: 'text' });
  }
  public createTask(task: task, headers: HttpHeaders) {
    //return this.httpclient
    //.post(this.baseUrl + 'Tasks')
  }
  public updateTask(inputtask: task, headers: HttpHeaders) {
    console.warn("updating task");
    console.warn(JSON.stringify(inputtask).toString());
    return this.httpclient.put(this.baseUrl + 'Tasks/' + inputtask.taskId, JSON.stringify(inputtask), { headers: headers });
  }
}

