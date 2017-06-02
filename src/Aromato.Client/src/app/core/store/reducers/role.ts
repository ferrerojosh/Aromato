import { Role } from '../../models/role';
import { Actions, LOAD, LOAD_FAILURE, LOAD_SUCCESS } from '../actions/role';

export interface State {
  roles: Role[];
  loaded: boolean;
  loading: boolean;
}

export const initialState: State = {
  roles: [],
  loaded: false,
  loading: false,
};

export function reducer(state = initialState, action: Actions) {
  switch (action.type) {
    case LOAD:
      return Object.assign({}, state, {
        loading: true
      });
    case LOAD_SUCCESS:
      return {
        roles: action.roles,
        loading: false,
        loaded: true,
      };
    case LOAD_FAILURE:
      return Object.assign({}, state, {
        loading: false
      });
    default:
      return state;
  }
}

export const loading = (state: State) => state.loading;
export const loaded = (state: State) => state.loaded;
export const roles = (state: State) => state.roles;
