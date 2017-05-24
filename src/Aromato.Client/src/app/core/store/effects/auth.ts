import { Injectable } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Actions, Effect, toPayload } from '@ngrx/effects';
import { Observable } from 'rxjs/Observable';
import { Action } from '@ngrx/store';

import * as auth from '../../store/actions/auth';
import { environment } from '../../../../environments/environment';

@Injectable()
export class AuthEffects {

  @Effect()
  authorizeAction$: Observable<Action> = this.actions$
    .ofType(auth.AUTHORIZE)
    .switchMap((action: auth.AuthorizeAction) => {
        this.authService.issuer = environment.authServer;
        return this.authService.login(action.username, action.password, action.scopes)
          .map(res => ({
            type: auth.AUTHORIZE_SUCCESS,
            accessToken: this.authService.accessToken()
          }))
          .catch(err => Observable.of({ type: auth.AUTHORIZE_FAILURE, payload: err }));
      }
    );

  @Effect()
  authenticateAction$: Observable<Action> = this.actions$
    .ofType(auth.AUTHENTICATE)
    .switchMap((action: auth.AuthenticateAction) => {
      this.authService.issuer = environment.authServer;
      return this.authService.login(action.username, action.password, action.scopes)
        .map(res => ({
          type: auth.AUTHENTICATE_SUCCESS,
          accessToken: this.authService.accessToken(),
          identityClaims: this.authService.identityClaims()
        }))
        .catch(err => Observable.of({ type: auth.AUTHENTICATE_FAILURE, payload: err }));
    });

  constructor(private actions$: Actions,
              private authService: AuthService) {
  }
}
