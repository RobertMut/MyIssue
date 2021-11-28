import { Component, OnInit } from '@angular/core';
import { ClientService } from "../../../services/ClientService";
import { AuthService } from "../../../services/AuthService";
import { HttpErrorResponse } from '@angular/common/http';
import { ClientRoot, Client } from "../../../models/Client";
import { map } from 'rxjs';

@Component({
  selector: 'app-create-client',
  templateUrl: './create-client.component.html',
  styleUrls: ['./create-client.component.css'],
  providers: [ClientService]
})
export class CreateClientComponent implements OnInit {
  public show: false;
  public clients;
  public result: string;
  constructor(private clientService: ClientService,
    private auth: AuthService) {
    this.clientService.getClients(this.auth.headers()).subscribe(
      {
        next: (v) => this.clients = (v as ClientRoot),
        error: (e) => {
          console.error(e);
          this.auth.CheckUnauthorized(e);
        }
      }
    );
  }
  ngOnInit(): void {
  }
  public createClient(clientFormValues: any) {
    let client: Client = {
      id: null,
      name: clientFormValues.name,
      country: clientFormValues.country,
      no: clientFormValues.no,
      street: clientFormValues.street,
      streetNo: clientFormValues.streetNo,
      flatNo: clientFormValues.flatNo,
      description: clientFormValues.description
    }
    this.clientService.postClient(client, this.auth.headers()).subscribe(error => {
        this.auth.CheckUnauthorized(error);
        if ((error as HttpErrorResponse).statusText == 'OK') {
          this.result = 'New client posted. Refresh page to see results!';
        }
      });
  }

}
