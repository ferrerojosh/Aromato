import { Injectable } from '@angular/core';
import { Actions, Effect } from '@ngrx/effects';
import { RoleService } from '../../services/role.service';

import * as role from '../actions/role';
import { LoadAction } from '../actions/role';
import { Role } from '../../models/role';
import { of } from 'rxjs/observable/of';

@Injectable()
export class RoleEffects {

  @Effect()
  loadRoles$ = this.actions$
    .ofType(role.LOAD)
    .map((action: LoadAction) => action.username)
    .debounceTime(500)
    .mergeMap(username => this.roleService
      .findByUsername(username)
      .map((roles: Role[]) => new role.LoadSuccessAction(roles))
      .catch(error => of(new role.LoadFailureAction()))
    );

  constructor(private actions$: Actions,
              private roleService: RoleService) {}
}
