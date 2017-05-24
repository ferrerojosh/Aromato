import { Injectable } from '@angular/core';
import { InventoryService } from '../../services/inventory.service';
import { Observable } from 'rxjs/Observable';
import { Action } from '@ngrx/store';
import { Actions, Effect } from '@ngrx/effects';

import * as inventory from '../actions/inventory';
import { Inventory } from '../../models/inventory';
import { of } from 'rxjs/observable/of';

@Injectable()
export class InventoryEffects {
  @Effect()
  loadInventory$: Observable<Action> = this.actions$
    .ofType(inventory.LOAD)
    .switchMap(() =>
      this.inventoryService
        .findAll()
        .map((inventories: Inventory[]) => new inventory.InventoryLoadSuccessAction(inventories))
        .catch(error => of(new inventory.InventoryLoadFailureAction(error)))
    );

  constructor(private actions$: Actions,
              private inventoryService: InventoryService) {}
}
