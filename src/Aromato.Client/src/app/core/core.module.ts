import { NgModule, Provider } from '@angular/core';
import { CommonModule } from '@angular/common';
import { reducer } from './store/reducers/index';
import { INITIAL_STATE, StoreModule } from '@ngrx/store';
import { RouterStoreModule } from '@ngrx/router-store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { AuthGuard } from './guards/auth-guard.service';
import { EmployeeService } from './services/employee.service';
import { InventoryService } from './services/inventory.service';
import { RoleService } from './services/role.service';
import { AuthService } from './services/auth.service';
import { EffectsModule } from '@ngrx/effects';
import { AuthEffects } from './store/effects/auth';

import * as authService from './services/auth.service';
import { InventoryEffects } from './store/effects/inventory';
import { EmployeeEffects } from './store/effects/employee';

const accessClaims = JSON.parse(localStorage.getItem(authService.ACCESS_TOKEN_CLAIMS_STORAGE) || '{}');
const isAuthorized: boolean = !accessClaims.scope ? false : accessClaims.scope.length > 2;
const isAuthenticated: boolean = !accessClaims.scope ? false : accessClaims.scope.some(scope => scope == 'roles');

export const rehydrateState: Provider = {
  provide: INITIAL_STATE,
  useValue: {
    auth: {
      accessToken: localStorage.getItem(authService.ACCESS_TOKEN_STORAGE) || {},
      identity: JSON.parse(localStorage.getItem(authService.IDENTITY_TOKEN_CLAIMS_STORAGE) || '{}'),
      authorized: isAuthorized,
      authenticated: isAuthenticated
    }
  }
};

@NgModule({
  imports: [
    CommonModule,
    StoreModule.provideStore(reducer),
    RouterStoreModule.connectRouter(),
    EffectsModule.runAfterBootstrap(AuthEffects),
    EffectsModule.runAfterBootstrap(InventoryEffects),
    EffectsModule.runAfterBootstrap(EmployeeEffects),
    StoreDevtoolsModule.instrumentOnlyWithExtension()
  ],
  exports: [
    CommonModule,
    StoreModule,
    RouterStoreModule,
    EffectsModule
  ],
  providers: [
    AuthService,
    RoleService,
    InventoryService,
    EmployeeService,
    AuthGuard,
    rehydrateState
  ],
  declarations: []
})
export class CoreModule { }
