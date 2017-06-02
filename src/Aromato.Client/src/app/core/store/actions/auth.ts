import { Action } from '@ngrx/store';

export const AUTHENTICATE = '[Auth] Authenticate';
export const AUTHENTICATE_SUCCESS = '[Auth] Authenticate Success';
export const AUTHENTICATE_FAILURE = '[Auth] Authenticate Failure';
export const LOAD_FROM_DB = '[Auth] Load Token from DB';
export const LOAD_FROM_DB_SUCCESS = '[Auth] Load Token from DB Success';
export const LOAD_FROM_DB_FAILURE = '[Auth] Load Token from DB Failure';
export const LOGOUT = '[Auth] Logout';

export class AuthenticateAction implements Action {
  readonly type = AUTHENTICATE;

  constructor(public username: string, public password: string, public scopes: string[]) {}
}

export class AuthenticateSuccessAction implements Action {
  readonly type = AUTHENTICATE_SUCCESS;

  constructor(public identityClaims: object, public accessToken: string) {}
}

export class LoadFromDatabaseAction implements Action {
  readonly type = LOAD_FROM_DB;

  constructor() {}
}

export class LoadFromDatabaseSuccessAction implements Action {
  readonly type = LOAD_FROM_DB_SUCCESS;

  constructor(public identityClaims: object, public accessToken: string) {}
}

export class LoadFromDatabaseFailureAction implements Action {
  readonly type = LOAD_FROM_DB_FAILURE;

  constructor(public error: any) {}
}
export class AuthenticateFailureAction implements Action {
  readonly type = AUTHENTICATE_FAILURE;

  constructor(public error: any) {}
}

export class LogoutAction implements Action {
  readonly type = LOGOUT;
}

export type Actions
  = AuthenticateAction
  | AuthenticateSuccessAction
  | AuthenticateFailureAction
  | LogoutAction
  | LoadFromDatabaseAction
  | LoadFromDatabaseFailureAction
  | LoadFromDatabaseSuccessAction;
