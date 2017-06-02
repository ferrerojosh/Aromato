import { ActionReducer, combineReducers } from '@ngrx/store';
import { compose } from '@ngrx/core/compose';
import { storeFreeze } from 'ngrx-store-freeze';
import { environment } from '../../../../environments/environment';

import * as fromRouter from '@ngrx/router-store';
import * as fromLayout from './layout';
import * as fromAuth from './auth';
import * as fromRole from './role';

import { createSelector } from 'reselect';

export interface State {
  router: fromRouter.RouterState;
  layout: fromLayout.State;
  auth: fromAuth.State;
  role: fromRole.State;
}

export const initialState: State = {
  router: fromRouter.initialState,
  layout: fromLayout.initialState,
  auth: fromAuth.initialState,
  role: fromRole.initialState,
};

const reducers = {
  router: fromRouter.routerReducer,
  layout: fromLayout.reducer,
  auth: fromAuth.reducer,
  role: fromRole.reducer,
};

const developmentReducer: ActionReducer<State> = compose(storeFreeze, combineReducers)(reducers);
const productionReducer: ActionReducer<State> = combineReducers(reducers);

export function reducer(state: any, action: any) {
  if (environment.production) {
    return productionReducer(state, action);
  } else {
    return developmentReducer(state, action);
  }
}

export const routerState = (state: State) => state.router;
export const layoutState = (state: State) => state.layout;
export const authState = (state: State) => state.auth;
export const roleState = (state: State) => state.role;

export const getAccessToken = createSelector(authState, fromAuth.accessToken);
export const getRoles = createSelector(roleState, fromRole.roles);

export const areRolesLoaded = createSelector(roleState, fromRole.loaded);
export const areRolesLoading = createSelector(roleState, fromRole.loading);
export const isAuthLoading = createSelector(authState, fromAuth.loading);
export const onAuthError = createSelector(authState, fromAuth.error);
export const isAuthenticated = createSelector(authState, fromAuth.authenticated);
export const isSideNavOpened = createSelector(layoutState, fromLayout.isSideNavOpened);
