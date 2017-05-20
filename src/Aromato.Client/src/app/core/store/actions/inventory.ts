import { Action } from '@ngrx/store';

export const LOAD = '[Inventory] Load';
export const LOAD_SUCCESS = '[Inventory] Load Success';
export const LOAD_FAILURE = '[Inventory] Load Failure';

export class InventoryLoadAction implements Action {
  readonly type = LOAD;
}

export class InventoryLoadSuccessAction implements Action {
  readonly type = LOAD_SUCCESS;
}

export class InventoryLoadFailureAction implements Action {
  readonly type = LOAD_FAILURE;
}

export type Actions
  = InventoryLoadAction
  | InventoryLoadFailureAction
  | InventoryLoadSuccessAction;
