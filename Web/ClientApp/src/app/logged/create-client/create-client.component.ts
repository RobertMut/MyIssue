import { Component, OnInit } from '@angular/core';
import { ClientService } from "../../../services/ClientService";
import { IClient, IClientRoot } from "../../../interfaces/Client";
import { AuthService } from "../../../services/AuthService";
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-create-client',
  templateUrl: './create-client.component.html',
  styleUrls: ['./create-client.component.css'],
  providers: [ClientService]
})
export class CreateClientComponent implements OnInit {
  public show: false;
  public clients: IClientRoot = {} as IClientRoot;
  public result: string;
  constructor(private clientService: ClientService,
    private auth: AuthService) {
    this.clientService.getClients(this.auth.headers()).subscribe(result => {
      this.clients = JSON.parse(result);
    },
      error => {
        console.error(error);
        this.auth.CheckUnauthorized(error);
      });
  }

  ngOnInit(): void {
  }
  public createClient(clientFormValues: any) {
    let client: IClient = {
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
