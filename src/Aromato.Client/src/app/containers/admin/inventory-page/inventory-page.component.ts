import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Inventory } from '../../../core/models/inventory';
import { Store } from '@ngrx/store';

import * as fromRoot from '../../../core/store/reducers';
import * as inventory from '../../../core/store/actions/inventory';

@Component({
  selector: 'app-inventory-page',
  templateUrl: './inventory-page.component.html',
  styleUrls: ['./inventory-page.component.scss']
})
export class InventoryPageComponent implements OnInit {
  inventories$: Observable<Inventory[]>;

  constructor(private store: Store<fromRoot.AppState>) { }

  ngOnInit() {
    this.inventories$ = this.store.select(fromRoot.inventories);
    this.store.dispatch(new inventory.InventoryLoadAction());
  }

}
