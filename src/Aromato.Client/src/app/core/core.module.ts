import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { reducer } from './store/reducers/index';
import { StoreModule } from '@ngrx/store';
import { RouterStoreModule } from '@ngrx/router-store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { AuthGuard } from './guards/auth-guard.service';
import { EmployeeService } from './services/employee.service';
import { InventoryService } from './services/inventory.service';
import { RoleService } from './services/role.service';
import { AuthService } from './services/auth.service';

@NgModule({
  imports: [
    CommonModule,
    StoreModule.provideStore(reducer),
    RouterStoreModule.connectRouter(),
    StoreDevtoolsModule.instrumentOnlyWithExtension()
  ],
  exports: [
    CommonModule,
    StoreModule,
    RouterStoreModule
  ],
  providers: [
    AuthService,
    RoleService,
    InventoryService,
    EmployeeService,
    AuthGuard
  ],
  declarations: []
})
export class CoreModule { }
