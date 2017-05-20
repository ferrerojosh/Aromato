import { Action } from '@ngrx/store';

export const AUTHENTICATE = '[Auth] Authenticate';
export const AUTHORIZE = '[Auth] Authorize';
export const LOGOUT = '[Auth] Logout';

export class AuthenticateAction implements Action {
  readonly type = AUTHENTICATE;

  constructor(public identityClaims: object, public accessToken: string) {}
}

export class AuthorizeAction implements Action {
  readonly type = AUTHORIZE;

  constructor(public accessToken: string) {}
}

export class LogoutAction implements Action {
  readonly type = LOGOUT;
}

export type Actions
  = AuthenticateAction
  | AuthorizeAction
  | LogoutAction;

