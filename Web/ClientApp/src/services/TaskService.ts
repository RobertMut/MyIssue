import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ITask, ITaskroot } from "../models/task";


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
  public createTask(task: ITask, headers: HttpHeaders) {
    return this.httpclient.post(this.baseUrl + 'Tasks', JSON.stringify(task), { headers: headers });
  }
  public updateTask(inputtask: ITask, headers: HttpHeaders) {
    return this.httpclient.put(this.baseUrl + 'Tasks/' + inputtask.taskId, JSON.stringify(inputtask), { headers: headers });
  }
}

