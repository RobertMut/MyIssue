import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { map, Observable } from 'rxjs';
import { Client, ClientRoot } from "../models/Client";
import { AuthService } from "./AuthService";

@Injectable()
export class ClientService {
  private baseUrl: string;

  constructor(private httpclient: HttpClient,
    private auth: AuthService,
    @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  public getClients(headers: HttpHeaders) {
    return this.httpclient
      .get(this.baseUrl + 'Clients', { headers: headers, responseType: 'json'});
  }
  public postClient(client: Client, headers: HttpHeaders) {
    //console.warn(client.name + ' ' + this.baseUrl+'Clients/new');
    return this.httpclient
      .post(this.baseUrl + 'Clients/new', JSON.stringify(client), { headers: headers });
  }
}
