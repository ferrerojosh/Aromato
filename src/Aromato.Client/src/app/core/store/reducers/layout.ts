import * as layout from '../actions/layout';

export interface State {
  sideNavOpened: boolean;
}

export const initialState: State = {
  sideNavOpened: false,
};

export function reducer(state = initialState, action: layout.Actions): State {
  switch (action.type) {
    case layout.CLOSE_SIDENAV:
      return {
        sideNavOpened: false
      };
    case layout.OPEN_SIDENAV:
      return {
        sideNavOpened: true
      };
    default:
      return state;
  }
}

export const sideNavOpened = (state: State) => state.sideNavOpened;
