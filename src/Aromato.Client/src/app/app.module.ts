import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { EmployeeComponent } from './employee/employee.component';
import { InventoryComponent } from './inventory/inventory.component';
import { LoginComponent } from './login/login.component';
import { InventoryService } from "./inventory.service";
import { EmployeeService } from "./employee.service";
import { OAuthModule } from 'angular-oauth2-oidc';
import { LoginRoleComponent } from './login-role/login-role.component';
import { AppRoutingModule } from './app-routing.module';
import { RoleService } from './role.service';
import { AuthService } from './auth.service';

@NgModule({
  declarations: [
    AppComponent,
    EmployeeComponent,
    InventoryComponent,
    LoginComponent,
    LoginRoleComponent
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    FormsModule,
    HttpModule,
    AppRoutingModule,
  ],
  providers: [
    AuthService,
    InventoryService,
    EmployeeService,
    RoleService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
