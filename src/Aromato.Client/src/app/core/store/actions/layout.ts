import { Action } from '@ngrx/store';
export const TOGGLE_SIDENAV   = '[Layout] Toggle Sidenav';
export const LOAD_FROM_DB   = '[Layout] Load from DB';
export const LOAD_FROM_DB_SUCCESS = '[Layout] Load from DB Success';
export const LOAD_FROM_DB_FAILURE = '[Layout] Load from DB Failure';

export class ToggleSidenavAction implements Action {
  readonly type = TOGGLE_SIDENAV;

  constructor(public payload: boolean) {}
}

export class LoadFromDatabaseAction implements Action {
  readonly type = LOAD_FROM_DB;
}

export class LoadFromDatabaseSuccessAction implements Action {
  readonly type = LOAD_FROM_DB_SUCCESS;

  constructor(public payload: any) {}
}

export class LoadFromDatabaseFailureAction implements Action {
  readonly type = LOAD_FROM_DB_FAILURE;
}

export type Actions
  = ToggleSidenavAction
  | LoadFromDatabaseAction
  | LoadFromDatabaseSuccessAction
  | LoadFromDatabaseFailureAction;
