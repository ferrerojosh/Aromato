import { environment } from '../environments/environment';
import { Observable } from 'rxjs/Observable';
import { Employee } from './employee';
import { Injectable } from '@angular/core';
import { Http, RequestOptions, Response, Headers } from '@angular/http';
import { AuthService } from './auth.service';

@Injectable()
export class EmployeeService {

  constructor(private authService: AuthService, private http: Http) {

  }

  findAll(): Observable<Array<Employee>> {
    let headers = new Headers({
      "Authorization": this.authService.authorizationHeader()
    });
    let options = new RequestOptions({headers: headers});

    return this.http.get(environment.resourceServer + 'employee', options)
                    .map((res: Response) => res.json());
  }
}
