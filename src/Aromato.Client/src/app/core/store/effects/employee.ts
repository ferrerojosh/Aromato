import { Injectable } from '@angular/core';
import { Actions, Effect } from '@ngrx/effects';
import { EmployeeService } from '../../services/employee.service';

import * as employee from '../../store/actions/employee';
import { of } from 'rxjs/observable/of';
import { Employee } from '../../models/employee';

@Injectable()
export class EmployeeEffects {
  @Effect()
  loadEmployee$ = this.actions$
    .ofType(employee.LOAD)
    .switchMap(() =>
      this.employeeService
        .findAll()
        .map((employees: Employee[]) => new employee.EmployeeLoadSuccessAction(employees))
        .catch(error => of(new employee.EmployeeLoadFailureAction(error)))
    );

  constructor(private actions$: Actions,
              private employeeService: EmployeeService) {}
}
