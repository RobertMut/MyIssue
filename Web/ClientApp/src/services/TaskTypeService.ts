import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from 'rxjs';

@Injectable()
export class TaskTypeService {
  private baseUrl: string;

  constructor(private httpclient: HttpClient,
    @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  public getTaskTypes(headers: HttpHeaders): Observable<string> {
    return this.httpclient
      .get(this.baseUrl + 'TaskTypes', { headers: headers, responseType: 'text' });
  }
}
