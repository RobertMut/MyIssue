import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IPagedTaskRequest, ITask } from "../interfaces/Task";


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
  public getPagedFirst(pageNumber: number, pageSize: number, headers: HttpHeaders): Observable<string> {
    let pagedRequest: IPagedTaskRequest = {
      link: null,
      page: pageNumber,
      size: pageSize };
    return this.httpclient
      .post(this.baseUrl + 'Tasks/pagedFirst', JSON.stringify(pagedRequest), { headers: headers, responseType: 'text' });
  }
  public getPagedLink(pageLink: string, headers: HttpHeaders): Observable<string> {
    let pagedRequest: IPagedTaskRequest = {
      link: pageLink,
      page: null,
      size: null };
    return this.httpclient
      .post(this.baseUrl + 'Tasks/pagedLink', JSON.stringify(pagedRequest), { headers: headers, responseType: 'text' });
  }
  public createTask(task: ITask, headers: HttpHeaders) {
    return this.httpclient.post(this.baseUrl + 'Tasks', JSON.stringify(task), { headers: headers });
  }
  public updateTask(inputTask: ITask, headers: HttpHeaders) {
    return this.httpclient.put(this.baseUrl + 'Tasks/' + inputTask.taskId, JSON.stringify(inputTask), { headers: headers });
  }
}

