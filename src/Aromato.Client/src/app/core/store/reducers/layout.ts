import * as fromLayout from '../actions/layout';

export interface State {
  sideNavOpened: boolean;
}

export const initialState: State = {
  sideNavOpened: false
};

export function reducer(state = initialState, action: fromLayout.Actions) {
  switch(action.type) {
    case fromLayout.TOGGLE_SIDENAV:
      return {
        sideNavOpened: action.payload
      };
    case fromLayout.LOAD_FROM_DB_SUCCESS:
      return action.payload;
    default:
      return state;
  }
}

export const isSideNavOpened = (state: State) => state.sideNavOpened;
