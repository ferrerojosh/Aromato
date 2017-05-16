import { Component, OnInit } from '@angular/core';
import { Employee } from '../employee';
import { EmployeeService } from '../employee.service';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.scss']
})
export class EmployeeComponent implements OnInit {
  employees: Array<Employee>;

  constructor(private employeeService:EmployeeService) {}

  ngOnInit() {
    this.employeeService.findAll().subscribe(employees => {
      this.employees = employees;
    });
  }

}
