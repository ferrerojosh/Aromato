import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { CoreModule } from './core/core.module';
import { routes } from './app-routing';
import { environment } from '../environments/environment';

import { AppComponent } from './app.component';
import { DashboardPageComponent } from './containers/admin/dashboard-page/dashboard-page.component';
import { AdminComponent } from './containers/admin/admin.component';
import { MaterialModule } from '@angular/material';
import { NavbarComponent } from './components/admin/navbar/navbar.component';
import { TreeModule } from 'angular-tree-component';
import { InventoryPageComponent } from './containers/admin/inventory-page/inventory-page.component';
import { EmployeePageComponent } from './containers/admin/employee-page/employee-page.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LoginPageComponent } from './containers/login-page/login-page.component';
import { FlexLayoutModule } from '@angular/flex-layout';
import { SlimLoadingBarModule } from 'ng2-slim-loading-bar';
import { NotFoundPageComponent } from './containers/not-found-page/not-found-page.component';
import { LoginPage2Component } from './containers/login-page2/login-page2.component';
import { LoginComponent } from './components/login/login.component';
import { LoginRoleComponent } from './components/login-role/login-role.component';

const devModules = environment.production ? [] : [
  StoreDevtoolsModule.instrumentOnlyWithExtension(),
];

@NgModule({
  declarations: [
    AppComponent,
    DashboardPageComponent,
    AdminComponent,
    NavbarComponent,
    InventoryPageComponent,
    EmployeePageComponent,
    LoginPageComponent,
    NotFoundPageComponent,
    LoginPage2Component,
    LoginComponent,
    LoginRoleComponent
  ],
  imports: [
    RouterModule.forRoot(routes),
    CoreModule,
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    HttpModule,
    ReactiveFormsModule,
    FlexLayoutModule,
    MaterialModule,
    SlimLoadingBarModule.forRoot(),
    TreeModule,
    ... devModules
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
