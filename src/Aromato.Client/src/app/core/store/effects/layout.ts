import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Action } from '@ngrx/store';

import 'rxjs/add/operator/mergeMap';
import 'rxjs/add/operator/startWith';
import 'rxjs/add/operator/toPromise';

import * as fromLayout from '../actions/layout';
import { Actions, Effect } from '@ngrx/effects';
import { of } from 'rxjs/observable/of';

@Injectable()
export class LayoutEffects {

  @Effect()
  loadStateFromDb$: Observable<Action> = this.actions$
    .ofType(fromLayout.LOAD_FROM_DB)
    .startWith(new fromLayout.LoadFromDatabaseAction())
    .switchMap(() =>
      of(new fromLayout.LoadFromDatabaseSuccessAction({
        sideNavOpened: localStorage.getItem('sideNavOpened') === 'true' || false
      }))
    );

  constructor(
    private actions$: Actions
  ) { }
}
