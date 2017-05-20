import * as fromRouter from '@ngrx/router-store';
import * as fromLayout from './layout';
import * as fromAuth from './auth';

/**
 * The compose function is one of our most handy tools. In basic terms, you give
 * it any number of functions and it returns a function. This new function
 * takes a value and chains it through every composed function, returning
 * the output.
 *
 * More: https://drboolean.gitbooks.io/mostly-adequate-guide/content/ch5.html
 */
import { compose } from '@ngrx/core/compose';

/**
 * storeFreeze prevents state from being mutated. When mutation occurs, an
 * exception will be thrown. This is useful during development mode to
 * ensure that none of the reducers accidentally mutates the state.
 */
import { storeFreeze } from 'ngrx-store-freeze';

/**
 * combineReducers is another useful metareducer that takes a map of reducer
 * functions and creates a new reducer that gathers the values
 * of each reducer and stores them using the reducer's key. Think of it
 * almost like a database, where every reducer is a table in the db.
 *
 * More: https://egghead.io/lessons/javascript-redux-implementing-combinereducers-from-scratch
 */
import { ActionReducer, combineReducers } from '@ngrx/store';
import { environment } from '../../../../environments/environment';
import { createSelector } from 'reselect';

export interface AppState {
  router: fromRouter.RouterState;
  layout: fromLayout.State;
  auth: fromAuth.State;
}

const reducers = {
  router: fromRouter.routerReducer,
  layout: fromLayout.reducer,
  auth: fromAuth.reducer
};

const developmentReducer: ActionReducer<AppState> = compose(storeFreeze, combineReducers)(reducers);
const productionReducer: ActionReducer<AppState> = combineReducers(reducers);

export function reducer(state: any, action: any) {
  if (environment.production) {
    return productionReducer(state, action);
  } else {
    return developmentReducer(state, action);
  }
}

export const layoutState = (state: AppState) => state.layout;
export const authState = (state: AppState) => state.auth;

export const showSidenav = createSelector(layoutState, fromLayout.showSidenav);
export const isAuthorized = createSelector(authState, fromAuth.authorized);
export const isAuthenticated = createSelector(authState, fromAuth.authenticated);
export const accessToken = createSelector(authState, fromAuth.accessToken);
export const identity = createSelector(authState, fromAuth.identity);
