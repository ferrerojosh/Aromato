import { Injectable } from '@angular/core';
import { Http, RequestOptions, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Inventory } from '../models/inventory';
import { environment } from '../../../environments/environment';

import * as fromRoot from '../store/reducers';
import { Store } from '@ngrx/store';

@Injectable()
export class InventoryService {
  accessToken: string;

  constructor(private http: Http,
              private store: Store<fromRoot.AppState>) { }

  findAll(): Observable<Inventory[]> {
    this.store.select(fromRoot.accessToken).subscribe(accessToken => {
      this.accessToken = accessToken;
    });

    const headers = new Headers({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${this.accessToken}`
    });
    const options = new RequestOptions({ headers: headers });

    return this.http.get(environment.resourceServer + 'inventory', options).map(r => r.json());
  }

}
