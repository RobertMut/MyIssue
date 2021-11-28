import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from 'rxjs';
import { TaskTypeRoot } from "src/models/TaskType";

@Injectable()
export class TaskTypeService {
  private baseUrl: string;

  constructor(private httpclient: HttpClient,
    @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  public getTaskTypes(headers: HttpHeaders) {
    return this.httpclient
      .get<TaskTypeRoot>(this.baseUrl + 'TaskTypes', { headers: headers, responseType: 'json' });
  }
}
