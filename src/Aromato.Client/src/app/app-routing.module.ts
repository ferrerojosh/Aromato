import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { InventoryComponent } from './inventory/inventory.component';
import { EmployeeComponent } from './employee/employee.component';
import { LoginRoleComponent } from './login-role/login-role.component';

const appRoutes: Routes = [
  { path: 'home', component: LoginComponent },
  { path: 'employee', component: EmployeeComponent },
  { path: 'inventory', component: InventoryComponent},
  { path: 'login', component: LoginComponent },
  { path: 'authorize', component: LoginRoleComponent }
];

@NgModule({
  imports: [
    RouterModule.forRoot(appRoutes)
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
