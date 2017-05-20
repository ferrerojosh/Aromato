import * as layout from '../actions/layout';

export interface State {
  showSidenav: boolean;
}

export const initialState: State = {
  showSidenav: false,
};

export function reducer(state = initialState, action: layout.Actions): State {
  switch (action.type) {
    case layout.CLOSE_SIDENAV:
      return {
        showSidenav: false
      };
    case layout.OPEN_SIDENAV:
      return {
        showSidenav: true
      };
    default:
      return state;
  }
}

export const showSidenav = (state: State) => state.showSidenav;
