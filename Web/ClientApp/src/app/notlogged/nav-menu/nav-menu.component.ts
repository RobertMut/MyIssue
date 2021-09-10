import { Component } from '@angular/core';
import { Router, ActivatedRoute } from "@angular/router";
import { AuthService } from "../../../services/AuthService";

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  constructor(private router: Router, private helpers: AuthService, private activeroute: ActivatedRoute) { }

  ngOnInit() {
    console.warn("navigation to home");
    if (this.helpers.isAuthenticated()) {
      this.router.navigate(['../nav-menu-logged']);
    } else {
      this.router.navigate(['login'], { relativeTo: this.activeroute });
    }
  }
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
