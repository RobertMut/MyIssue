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
import { MatRadioModule } from "@angular/material/radio/radio-module";
import { MatSelectModule } from "@angular/material/select/select-module";
import { MatCheckboxModule } from "@angular/material/checkbox/checkbox-module";
import { MatIconModule } from "@angular/material/icon/icon-module";
import { MatCardModule } from "@angular/material/card/card-module";
import { SharedModule } from "../modules/SharedModule";

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    LogoutComponent,
    HomeComponent,
    NavMenuComponent,
    NavMenuLoggedComponent,
    TaskViewComponent
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
    MatNativeDateModule,
    NgxMatNativeDateModule,
    MatRadioModule,
    MatSelectModule,
    MatCheckboxModule,
    MatIconModule,
    MatCardModule,
    SharedModule

  ],
  bootstrap: [AppComponent]
})
export class AppModule {

}
