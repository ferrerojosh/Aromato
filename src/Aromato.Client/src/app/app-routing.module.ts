import { Routes } from '@angular/router';
import { HomePageComponent } from './containers/admin/home-page/home-page.component';
import { LoginPageComponent } from './containers/login-page/login-page.component';
import { InventoryPageComponent } from './containers/admin/inventory-page/inventory-page.component';
import { EmployeePageComponent } from './containers/admin/employee-page/employee-page.component';
import { AuthGuard } from './core/guards/auth-guard.service';

export const routes: Routes = [
  {
    path: 'admin',
    component: HomePageComponent,
    canActivate: [AuthGuard],
    children: [
      {
        path: 'inventory',
        component: InventoryPageComponent,
      },
      {
        path: 'employee',
        component: EmployeePageComponent,
      }
    ]
  },
  {
    path: 'login',
    component: LoginPageComponent
  }
];
