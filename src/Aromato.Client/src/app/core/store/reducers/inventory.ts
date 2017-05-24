import { Inventory } from '../../models/inventory';

import * as inventory from '../actions/inventory';

export interface State {
  inventories: Inventory[];
  loading: boolean;
  loaded: boolean;
  error: boolean;
}

export const initialState: State = {
  inventories: [],
  loading: false,
  loaded: false,
  error: false
};

export function reducer(state = initialState, action: inventory.Actions) {
  switch (action.type) {
    case inventory.LOAD:
      return Object.assign({}, state, {
        loading: true
      });
    case inventory.LOAD_SUCCESS:
      return {
        inventories: action.payload,
        loaded: true,
        loading: false,
        error: false
      };
    case inventory.LOAD_FAILURE:
      return Object.assign({}, initialState, {
        error: true
      });
    default:
      return state;
  }
}

export const inventories = (state: State) => state.inventories;
export const loading = (state: State) => state.loading;
export const loaded = (state: State) => state.loaded;
export const error = (state: State) => state.error;
