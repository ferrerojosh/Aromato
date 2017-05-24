import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { MaterialModule } from '@angular/material';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { routes } from './app-routing.module';
import { CoreModule } from './core/core.module';
import { FlexLayoutModule } from '@angular/flex-layout';
import { HomePageComponent } from './containers/admin/home-page/home-page.component';
import { LoginPageComponent } from './containers/login-page/login-page.component';
import { InventoryPageComponent } from './containers/admin/inventory-page/inventory-page.component';
import { EmployeePageComponent } from './containers/admin/employee-page/employee-page.component';
import { ToolbarComponent } from './components/toolbar/toolbar.component';
import { AuthorizeComponent } from './components/authorize/authorize.component';

@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    LoginPageComponent,
    InventoryPageComponent,
    EmployeePageComponent,
    ToolbarComponent,
    AuthorizeComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    FormsModule,
    MaterialModule,
    HttpModule,
    CoreModule,
    FlexLayoutModule,
    RouterModule.forRoot(routes)
  ],
  entryComponents: [
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
