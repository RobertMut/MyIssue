import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

import { ITask } from "../models/task.model";

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private baseUrl: string;

  constructor(private httpclient: HttpClient,
    @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }
  public getLastTasks(howMany: number): Observable<ITask[]> {
    return this.httpclient
      .get<ITask[]>(this.baseUrl + 'Tasks/'+ howMany);
  }
}
