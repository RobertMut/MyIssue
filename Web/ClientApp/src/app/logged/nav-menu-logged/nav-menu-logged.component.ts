import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-nav-menu-logged',
  templateUrl: './nav-menu-logged.component.html',
  styleUrls: ['./nav-menu-logged.component.css']
})
export class NavMenuLoggedComponent implements OnInit {
  public login: string;
  constructor() {
    this.login = localStorage.getItem("login");
  }

  ngOnInit() {
  }

}
