import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { LoginPageComponent } from './containers/login-page/login-page.component';
import { HomePageComponent } from './containers/home-page/home-page.component';
import { MaterialModule } from '@angular/material';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { routes } from './app-routing.module';
import { CoreModule } from './core/core.module';
import { ToolbarComponent } from './components/toolbar/toolbar.component';
import { AuthorizeComponent } from './components/authorize/authorize.component';
import { EmployeePageComponent } from './containers/employee-page/employee-page.component';
import { InventoryPageComponent } from './containers/inventory-page/inventory-page.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginPageComponent,
    HomePageComponent,
    ToolbarComponent,
    AuthorizeComponent,
    EmployeePageComponent,
    InventoryPageComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    FormsModule,
    MaterialModule,
    HttpModule,
    CoreModule,
    RouterModule.forRoot(routes)
  ],
  entryComponents: [
    AuthorizeComponent
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
