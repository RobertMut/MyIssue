import { Component } from '@angular/core';
import { AuthService as AuthHelper } from "../../../helpers/AuthService";
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  constructor(private helpers: AuthHelper, private router: Router, private activatedRoute: ActivatedRoute){}

  ngOnInit() {
    if (!this.helpers.isAuthenticated()) {
      this.router.navigate(['./logout'], {relativeTo: this.activatedRoute});
    }
  }
}
