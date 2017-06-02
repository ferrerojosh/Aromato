import { Routes } from '@angular/router';
import { DashboardPageComponent } from './containers/admin/dashboard-page/dashboard-page.component';
import { AdminComponent } from './containers/admin/admin.component';
import { EmployeePageComponent } from './containers/admin/employee-page/employee-page.component';
import { InventoryPageComponent } from './containers/admin/inventory-page/inventory-page.component';
import { LoginPageComponent } from './containers/login-page/login-page.component';
import { NotFoundPageComponent } from './containers/not-found-page/not-found-page.component';
import { AuthGuard } from './core/guards/auth.guard';
import { LoginPage2Component } from './containers/login-page2/login-page2.component';
import { LoginComponent } from './components/login/login.component';
import { LoginRoleComponent } from './components/login-role/login-role.component';

export const routes: Routes = [
  {
    path: 'admin',
    component: AdminComponent,
    canActivate: [ AuthGuard ],
    children: [
      {
        path: '',
        component: DashboardPageComponent,
        data: {
          toolbarTitle: 'Dashboard'
        }
      },
      {
        path: 'employee',
        component: EmployeePageComponent,
        data: {
          toolbarTitle: 'Employee'
        }
      },
      {
        path: 'inventory',
        component: InventoryPageComponent,
        data: {
          toolbarTitle: 'Inventory'
        }
      }
    ]
  },
  {
    path: '',
    component: LoginPageComponent,
  },
  {
    path: 'login',
    component: LoginPageComponent,
  },
  {
    path: '**',
    component: NotFoundPageComponent
  }
];
