import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthService } from './AuthService';


import { ITask } from "../models/task.model";


@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private baseUrl: string;

  constructor(private httpclient: HttpClient,
    private auth: AuthService,
    @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  public getLastTasks(howMany: number): Observable<ITask[]> {
    return this.httpclient
      .get<ITask[]>(this.baseUrl + 'Tasks/someOpen/' + howMany, { headers: this.auth.headers() });
  }
}
