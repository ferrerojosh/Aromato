import { Injectable } from '@angular/core';
import { Http, RequestOptions, Headers } from '@angular/http';
import { Inventory } from '../models/inventory';
import { Observable } from 'rxjs/Observable';
import { environment } from '../../../environments/environment';
import { Store } from '@ngrx/store';

import * as fromRoot from '../store/reducers';

@Injectable()
export class InventoryService {
  accessToken: string;

  constructor(private http: Http,
              private store: Store<fromRoot.State>) {
    this.store.select(fromRoot.getAccessToken).subscribe(accessToken => {
      this.accessToken = accessToken;
    });
  }

  findAll(): Observable<Inventory[]> {
    const headers = new Headers({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${this.accessToken}`
    });
    const options = new RequestOptions({ headers: headers });

    return this.http.get(environment.apiServer + 'inventory', options)
      .map(r => r.json());
  }

}
