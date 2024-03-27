import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { from, Observable } from 'rxjs';

import { Employee, SuccessPagedResult } from '../../models';

@Injectable({
  providedIn: 'root'
})

export class EmployeeService {

  resourceUrl = environment.apiUrl;
  employeeUrl = this.resourceUrl + 'Employee';
  constructor(private httpClient: HttpClient) { }

  getEmployees(name: string, email: string, pageSize: number, pageIndex: number): Observable<SuccessPagedResult<Employee>> {
    let url = this.employeeUrl + `/GetAllWithPager?page=${pageIndex}&pageSize=${pageSize}`;
    if (name)
      url += `&name=${name}`;

    if (email)
      url += `&email=${email}`;

    return this.httpClient.get<SuccessPagedResult<Employee>>(url);
  }

  deleteEmployee(id: number) {
    return this.httpClient.delete(this.employeeUrl + '/' + id + '/Remove');
  }

  getEmployee(id: number): Observable<Employee> {
    return this.httpClient.get<Employee>(this.employeeUrl + '/' + id + '/Get');
  }

  saveEmployee(emp: Partial<Employee>) {
    console.log(JSON.stringify(emp));
    if (emp.id && emp.id > 0)
      return this.httpClient.post(this.employeeUrl + '/' + emp.id + '/Edit', emp);
    else
      return this.httpClient.post(this.employeeUrl + '/Save', emp);
  }

}