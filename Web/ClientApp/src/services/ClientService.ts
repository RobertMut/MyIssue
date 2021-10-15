import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from 'rxjs';
import { IClient } from "../interfaces/Client";

@Injectable()
export class ClientService {
  private baseUrl: string;

  constructor(private httpclient: HttpClient,
    @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  public getClients(headers: HttpHeaders): Observable<string> {
    return this.httpclient
      .get(this.baseUrl + 'Clients', { headers: headers, responseType: 'text' });
  }
  public postClient(client: IClient, headers: HttpHeaders) {
    console.warn(client.name + ' ' + this.baseUrl+'Clients/new');
    return this.httpclient
      .post(this.baseUrl + 'Clients/new', JSON.stringify(client), { headers: headers });
  }
}
