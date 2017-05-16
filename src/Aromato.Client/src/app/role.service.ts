import { Injectable } from '@angular/core';
import { Http, Headers, Response, RequestOptions } from '@angular/http';
import { environment } from '../environments/environment';
import { Role } from './role';
import { Observable } from 'rxjs/Observable';
import { AuthService } from './auth.service';

@Injectable()
export class RoleService {

  constructor(private authService: AuthService, private http: Http) {

  }

  findAll(): Observable<Array<Role>> {
    let headers = new Headers({
      "Authorization": this.authService.authorizationHeader()
    });
    let options = new RequestOptions({headers: headers});

    return this.http.get(environment.resourceServer + 'role', options)
      .map((res: Response) => res.json());
  }
}
