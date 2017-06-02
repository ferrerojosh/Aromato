import { Injectable } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Actions, Effect } from '@ngrx/effects';
import { Observable } from 'rxjs/Observable';
import { Action } from '@ngrx/store';

import * as auth from '../../store/actions/auth';
@Injectable()
export class AuthEffects {

  @Effect()
  loadFromDb$: Observable<Action> = this.actions$
    .ofType(auth.LOAD_FROM_DB)
    .startWith(new auth.LoadFromDatabaseAction())
    .switchMap(() => {
      if (this.authService.accessTokenValid) {
        return Observable.of({
          type: auth.LOAD_FROM_DB_SUCCESS,
          accessToken: this.authService.accessToken,
          identityClaims: this.authService.identityClaims
        });
      }
      return Observable.of({
        type: auth.LOAD_FROM_DB_FAILURE
      });
    });

  @Effect()
  authenticateAction$: Observable<Action> = this.actions$
    .ofType(auth.AUTHENTICATE)
    .debounceTime(500)
    .switchMap((action: auth.AuthenticateAction) =>
      this.authService.login(action.username, action.password, action.scopes)
        .map(res => ({
          type: auth.AUTHENTICATE_SUCCESS,
          accessToken: this.authService.accessToken,
          identityClaims: this.authService.identityClaims
        }))
        .catch(err => Observable.of({ type: auth.AUTHENTICATE_FAILURE, payload: err }))
    );

  constructor(private actions$: Actions,
              private authService: AuthService) {
  }
}
