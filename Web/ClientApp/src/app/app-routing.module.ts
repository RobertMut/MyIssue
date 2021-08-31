import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { RouterModule, Routes } from "@angular/router";
import { LogoutComponent } from "./logged/logout/logout.component";
import { NavMenuComponent } from "./notlogged/nav-menu/nav-menu.component";
import { NavMenuLoggedComponent } from "./logged/nav-menu-logged/nav-menu-logged.component";
import { HomeComponent } from "./logged/home/home.component";
import { LoginComponent } from "./notlogged/login/login.component";


const routes: Routes = [
  {
    path: 'nav-menu-logged', component: NavMenuLoggedComponent, children: [
      { path: '', component: HomeComponent },
      { path: "logout", component: LogoutComponent }
    ]},
  {
    path: 'nav-menu', component: NavMenuComponent, children: [
      { path: 'login', component: LoginComponent }
      ]
  },
  { path: '', redirectTo: 'nav-menu', pathMatch: 'full' },
  ];
@NgModule({
  declarations: [],
  imports: [CommonModule, RouterModule.forRoot(routes), RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
