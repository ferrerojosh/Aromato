import * as auth from '../actions/auth';

export interface State {
  identity: object;
  accessToken: string;
  error: boolean;
  loading: boolean;
  authenticated: boolean;
}

export const initialState: State = {
  identity: {},
  accessToken: '',
  error: false,
  loading: false,
  authenticated: false,
};

export function reducer(state = initialState, action: auth.Actions): State {
  switch (action.type) {
    case auth.AUTHENTICATE:
      return Object.assign({}, state, {
        loading: true
      });
    case auth.LOAD_FROM_DB_SUCCESS:
    case auth.AUTHENTICATE_SUCCESS:
      return {
        identity: action.identityClaims,
        accessToken: action.accessToken,
        error: false,
        loading: false,
        authenticated: true,
      };
    case auth.AUTHENTICATE_FAILURE:
      return Object.assign({}, state, {
        error: true,
        authenticated: false,
        loading: false
      });
    case auth.LOGOUT:
      return initialState;
    default:
      return state;
  }
}

export const identity = (state: State) => state.identity;
export const error = (state: State) => state.error;
export const accessToken = (state: State) => state.accessToken;
export const loading = (state: State) => state.loading;
export const authenticated = (state: State) => state.authenticated;
