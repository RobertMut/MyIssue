import { Component, OnInit } from '@angular/core';
import { AuthService as AuthHelper } from "../../../services/AuthService";
import { Router, NavigationStart } from '@angular/router';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css']
})
export class LogoutComponent implements OnInit {

  constructor(private auth: AuthHelper, private router: Router) { }

  ngOnInit() {
    this.auth.logout();
    
    this.router.navigate(['../nav-menu']);
  }

}
