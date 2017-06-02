import { NgModule } from '@angular/core';
import { RouterStoreModule } from '@ngrx/router-store';
import { StoreModule } from '@ngrx/store';
import { reducer } from './store/reducers';
import { EffectsModule } from '@ngrx/effects';
import { LayoutEffects } from './store/effects/layout';
import { AuthService } from './services/auth.service';
import { AuthEffects } from './store/effects/auth';
import { RoleService } from './services/role.service';
import { EmployeeService } from './services/employee.service';
import { InventoryService } from './services/inventory.service';
import { RoleEffects } from './store/effects/role';
import { AuthGuard } from './guards/auth.guard';

@NgModule({
  imports: [
    StoreModule.provideStore(reducer),
    RouterStoreModule.connectRouter(),
    EffectsModule.run(LayoutEffects),
    EffectsModule.run(RoleEffects),
    EffectsModule.run(AuthEffects),
  ],
  exports: [
    StoreModule,
    RouterStoreModule,
    EffectsModule,
  ],
  providers: [
    AuthGuard,
    AuthService,
    RoleService,
    EmployeeService,
    InventoryService
  ],
  declarations: []
})
export class CoreModule { }
