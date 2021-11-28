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
  getLinks(){
    
    var navigation = document.getElementsByClassName("links")[0];
    if (navigation.classList.contains("showNavigation")){
      navigation.classList.remove("showNavigation");
      navigation.classList.add("hideNavigation");
    } else if (navigation.classList.contains("hideNavigation")) {
      navigation.classList.remove("hideNavigation");
      navigation.classList.add("showNavigation");
    } else {
      document.getElementsByClassName("links")[0].classList.add("showNavigation");
    }
    //const navigation = 
  }
}
