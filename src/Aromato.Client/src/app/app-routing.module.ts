import { Routes } from '@angular/router';
import { HomePageComponent } from './containers/home-page/home-page.component';
import { LoginPageComponent } from './containers/login-page/login-page.component';
import { InventoryPageComponent } from './containers/inventory-page/inventory-page.component';
import { EmployeePageComponent } from './containers/employee-page/employee-page.component';
import { AuthGuard } from './core/guards/auth-guard.service';

export const routes: Routes = [
  {
    path: '',
    component: HomePageComponent
  },
  {
    path: 'login',
    component: LoginPageComponent
  },
  {
    path: 'inventory',
    component: InventoryPageComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'employee',
    component: EmployeePageComponent,
    canActivate: [AuthGuard],
  }
];
