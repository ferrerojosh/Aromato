import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Http, RequestOptions, Headers } from '@angular/http';
import { Employee } from '../models/employee';
import { Observable } from 'rxjs/Observable';

import * as fromRoot from '../store/reducers';
import { Store } from '@ngrx/store';

@Injectable()
export class EmployeeService {
  private accessToken: string;

  constructor(private http: Http,
              private store: Store<fromRoot.AppState>) { }

  findAll(): Observable<Employee[]> {
    this.store.select(fromRoot.accessToken).subscribe(accessToken => {
      this.accessToken = accessToken;
    });

    const headers = new Headers({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${this.accessToken}`
    });
    const options = new RequestOptions({ headers: headers });

    return this.http.get(environment.resourceServer + 'employee', options).map(r => r.json());
  }

}
