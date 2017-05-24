import * as auth from '../actions/auth';

export interface State {
  identity: object;
  accessToken: string;
  authorized: boolean;
  authenticated: boolean;
}

export const initialState: State = {
  identity: {},
  accessToken: '',
  authorized: false,
  authenticated: false,
};

export function reducer(state = initialState, action: auth.Actions): State {
  switch (action.type) {
    case auth.AUTHENTICATE_SUCCESS:
      return {
        identity: action.identityClaims,
        accessToken: action.accessToken,
        authorized: false,
        authenticated: true,
      };
    case auth.AUTHORIZE_SUCCESS:
      return {
        identity: state.identity,
        accessToken: action.accessToken,
        authorized: true,
        authenticated: true
      };
    case auth.LOGOUT:
      return {
        identity: {},
        accessToken: '',
        authorized: false,
        authenticated: false
      };
    default:
      return state;
  }
}

export const identity = (state: State) => state.identity;
export const accessToken = (state: State) => state.accessToken;
export const authorized = (state: State) => state.authorized;
export const authenticated = (state: State) => state.authenticated;
