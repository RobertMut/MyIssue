import { Component } from '@angular/core';
import { Router, ActivatedRoute } from "@angular/router";
import { AuthService } from "../../../services/AuthService";
import { TaskService } from "../../../services/TaskService";

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css'],
  providers: [TaskService]
})
export class NavMenuComponent {
  constructor(private router: Router, private auth: AuthService, private activeroute: ActivatedRoute) {
    this.router.navigate(['./login'], { relativeTo: this.activeroute });
  }

  ngOnInit() {
    this.auth.tokenlogin().subscribe((value: boolean) => {
      if (value) {
        //console.warn("VALUE "+value);
        this.router.navigate(['./../nav-menu-logged']);
      }
    });
  }
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
