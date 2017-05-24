import { Action } from '@ngrx/store';

export const AUTHENTICATE = '[Auth] Authenticate';
export const AUTHENTICATE_SUCCESS = '[Auth] Authenticate Success';
export const AUTHENTICATE_FAILURE = '[Auth] Authenticate Failure';
export const AUTHORIZE = '[Auth] Authorize';
export const AUTHORIZE_SUCCESS = '[Auth] Authorize Success';
export const AUTHORIZE_FAILURE = '[Auth] Authorize Failure';
export const LOGOUT = '[Auth] Logout';

export class AuthenticateAction implements Action {
  readonly type = AUTHENTICATE;

  constructor(public username: string, public password: string, public scopes: string[]) {}
}

export class AuthenticateSuccessAction implements Action {
  readonly type = AUTHENTICATE_SUCCESS;

  constructor(public identityClaims: object, public accessToken: string) {}
}

export class AuthenticateFailureAction implements Action {
  readonly type = AUTHENTICATE_FAILURE;

  constructor(public error: any) {}
}

export class AuthorizeAction implements Action {
  readonly type = AUTHORIZE;

  constructor(public username: string, public password: string, public scopes: string[]) {}
}

export class AuthorizeSuccessAction implements Action {
  readonly type = AUTHORIZE_SUCCESS;

  constructor(public accessToken: string) {}
}

export class AuthorizeFailureAction implements Action {
  readonly type = AUTHORIZE_FAILURE;

  constructor(public error: any) {}
}

export class LogoutAction implements Action {
  readonly type = LOGOUT;
}

export type Actions
  = AuthenticateAction
  | AuthenticateSuccessAction
  | AuthenticateFailureAction
  | AuthorizeAction
  | AuthorizeSuccessAction
  | AuthorizeFailureAction
  | LogoutAction;

