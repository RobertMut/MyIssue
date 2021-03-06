import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import {
  RouterModule,
  Routes
} from '@angular/router';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { LoginComponent } from "./notlogged/login/login.component";
import { LogoutComponent } from "./logged/logout/logout.component";
import { HomeComponent } from "./logged/home/home.component";
import { NavMenuComponent } from "./notlogged/nav-menu/nav-menu.component";
import { NavMenuLoggedComponent } from "./logged/nav-menu-logged/nav-menu-logged.component";
import { ReactiveFormsModule } from '@angular/forms';
import { TaskViewComponent } from './logged/task-view/task-view.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatNativeDateModule } from '@angular/material/core';
import { MatButtonModule } from '@angular/material/button';
import {
  NgxMatDatetimePickerModule,
  NgxMatNativeDateModule,
  NgxMatTimepickerModule
} from '@angular-material-components/datetime-picker';
import { MatRadioModule } from "@angular/material/radio";
import { MatSelectModule } from "@angular/material/select";
import { MatCheckboxModule } from "@angular/material/checkbox";
import { MatExpansionModule } from '@angular/material/expansion';
import { MatIconModule } from "@angular/material/icon";
import { MatCardModule } from "@angular/material/card";
import { SharedModule } from "../modules/SharedModule";
import { NewTaskComponent } from './logged/new-task/new-task.component';
import { TaskPagedComponent } from './logged/task-paged/task-paged.component';
import { ProfileComponent } from './logged/profile/profile.component';
import { CreateClientComponent } from './logged/create-client/create-client.component';
import { CreateUserComponent } from './logged/create-user/create-user.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    LogoutComponent,
    HomeComponent,
    NavMenuComponent,
    NavMenuLoggedComponent,
    TaskViewComponent,
    NewTaskComponent,
    TaskPagedComponent,
    ProfileComponent,
    CreateClientComponent,
    CreateUserComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatDatepickerModule,
    MatInputModule,
    NgxMatTimepickerModule,
    MatButtonModule,
    NgxMatDatetimePickerModule,
    MatFormFieldModule,
    MatExpansionModule,
    MatNativeDateModule,
    NgxMatNativeDateModule,
    MatSelectModule,
    MatRadioModule,
    MatCheckboxModule,
    MatIconModule,
    MatCardModule,
    SharedModule

  ],
  bootstrap: [AppComponent]
})
export class AppModule {

}
