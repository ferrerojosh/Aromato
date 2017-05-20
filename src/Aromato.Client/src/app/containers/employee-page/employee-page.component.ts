import { Component, OnInit } from '@angular/core';
import { Employee } from '../../core/models/employee';
import { Observable } from 'rxjs/Observable';
import { EmployeeService } from '../../core/services/employee.service';

@Component({
  selector: 'app-employee-page',
  templateUrl: './employee-page.component.html',
  styleUrls: ['./employee-page.component.scss']
})
export class EmployeePageComponent implements OnInit {
  employees$: Observable<Employee[]>;

  constructor(private employeeService: EmployeeService) { }

  ngOnInit() {
    this.employees$ = this.employeeService.findAll();
  }

}
