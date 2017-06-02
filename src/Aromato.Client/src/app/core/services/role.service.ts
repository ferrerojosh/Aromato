import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Http, RequestOptions, Headers } from '@angular/http';
import { Role } from '../models/role';
import { Observable } from 'rxjs/Observable';
import { Store } from '@ngrx/store';

import * as fromRoot from '../store/reducers';

@Injectable()
export class RoleService {
  accessToken: string;

  constructor(private http: Http,
              private store: Store<fromRoot.State>) {
    this.store.select(fromRoot.getAccessToken).subscribe(accessToken => {
      this.accessToken = accessToken;
    });
  }

  findByUsername(username: string): Observable<Role[]> {
    return this.http.get(environment.apiServer + `role/${username}`)
      .map(r => r.json());
  }

  findAll(): Observable<Role[]> {
    return this.http.get(environment.apiServer + 'role')
      .map(r => r.json());
  }
}
