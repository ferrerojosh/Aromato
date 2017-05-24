import { Action } from '@ngrx/store';
import { Employee } from '../../models/employee';

export const LOAD = '[Employee] Load';
export const LOAD_SUCCESS = '[Employee] Load Success';
export const LOAD_FAILURE = '[Employee] Load Failure';

export class EmployeeLoadAction implements Action {
  readonly type = LOAD;
}

export class EmployeeLoadSuccessAction implements Action {
  readonly type = LOAD_SUCCESS;

  constructor(public payload: Employee[]) {}
}

export class EmployeeLoadFailureAction implements Action {
  readonly type = LOAD_FAILURE;

  constructor(public payload: any) {};
}

export type Actions
  = EmployeeLoadAction
  | EmployeeLoadFailureAction
  | EmployeeLoadSuccessAction;
