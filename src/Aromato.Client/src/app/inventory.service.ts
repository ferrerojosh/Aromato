import { Injectable } from '@angular/core';
import { Http, Headers, Response, RequestOptions } from '@angular/http';
import { environment } from '../environments/environment';
import { Inventory } from './inventory';
import { Observable } from 'rxjs/Observable';
import { AuthService } from './auth.service';

@Injectable()
export class InventoryService {

  constructor(private authService: AuthService, private http: Http) { }

  findAll(): Observable<Array<Inventory>> {
    let headers = new Headers({
      "Authorization": this.authService.authorizationHeader()
    });
    let options = new RequestOptions({headers: headers});

    return this.http.get(environment.resourceServer + 'inventory', options)
      .map((res: Response) => res.json());
  }

}
