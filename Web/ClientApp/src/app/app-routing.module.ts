import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { RouterModule, Routes } from "@angular/router";
import { LogoutComponent } from "./logged/logout/logout.component";
import { NavMenuComponent } from "./notlogged/nav-menu/nav-menu.component";
import { NavMenuLoggedComponent } from "./logged/nav-menu-logged/nav-menu-logged.component";
import { HomeComponent } from "./logged/home/home.component";
import { LoginComponent } from "./notlogged/login/login.component";
import { TaskViewComponent } from "./logged/task-view/task-view.component";
import { NewTaskComponent } from "./logged/new-task/new-task.component";
import { TaskPagedComponent } from "./logged/task-paged/task-paged.component";
import { ProfileComponent } from "./logged/profile/profile.component";
import { CreateClientComponent } from "./logged/create-client/create-client.component";
import { CreateUserComponent } from "./logged/create-user/create-user.component";


const routes: Routes = [
  {
    path: 'nav-menu-logged', component: NavMenuLoggedComponent, children: [
      { path: '', component: HomeComponent },
      { path: 'logout', component: LogoutComponent },
      { path: 'task-view/:id', component: TaskViewComponent },
      { path: 'new-task', component: NewTaskComponent },
      { path: 'task-paged', component: TaskPagedComponent },
      { path: 'profile', component: ProfileComponent },
      { path: 'create-client', component: CreateClientComponent },
      { path: 'create-user', component: CreateUserComponent }
    ]
  },
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
