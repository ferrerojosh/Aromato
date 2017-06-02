import { Action } from '@ngrx/store';
import { Role } from '../../models/role';

export const LOAD         = '[Role] Load';
export const LOAD_SUCCESS = '[Role] Load Success';
export const LOAD_FAILURE = '[Role] Load Failure';

export class LoadAction implements Action {
  readonly type = LOAD;

  constructor(public username: string) {}
}

export class LoadSuccessAction implements Action {
  readonly type = LOAD_SUCCESS;

  constructor(public roles: Role[]) {}
}

export class LoadFailureAction implements Action {
  readonly type = LOAD_FAILURE;
}

export type Actions
  = LoadAction
  | LoadSuccessAction
  | LoadFailureAction;
