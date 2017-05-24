import { Employee } from '../../models/employee';

import * as employee from '../../store/actions/employee';

export interface State {
  employees: Employee[];
  loading: boolean;
  loaded: boolean;
  error: boolean;
}

export const initialState: State = {
  employees: [],
  loading: false,
  loaded: false,
  error: false
};

export function reducer(state = initialState, action: employee.Actions) {
  switch (action.type) {
    case employee.LOAD:
      return Object.assign({}, state, {
        loading: true
      });
    case employee.LOAD_SUCCESS:
      return {
        employees: action.payload,
        loaded: true,
        loading: false,
        error: false
      };
    case employee.LOAD_FAILURE:
      return Object.assign({}, initialState, {
        error: true
      });
    default:
      return state;
  }
}

export const employees = (state: State) => state.employees;
export const loading = (state: State) => state.loading;
export const loaded = (state: State) => state.loaded;
export const error = (state: State) => state.error;
