import { Component, OnInit } from '@angular/core';
import * as fromRoot from '../../../core/store/reducers';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs/Observable';
import { Employee } from '../../../core/models/employee';
import { EmployeeLoadAction } from '../../../core/store/actions/employee';

@Component({
  selector: 'app-employee-page',
  templateUrl: './employee-page.component.html',
  styleUrls: ['./employee-page.component.scss']
})
export class EmployeePageComponent implements OnInit {
  employees$: Observable<Employee[]>;

  constructor(private store: Store<fromRoot.AppState>) { }

  ngOnInit() {
    this.employees$ = this.store.select(fromRoot.employees);
    this.store.dispatch(new EmployeeLoadAction());
  }

}
