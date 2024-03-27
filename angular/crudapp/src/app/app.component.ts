import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { AddEditEmployeesComponent } from './employee/add-edit-employees/add-edit-employees.component';
import { EmployeeService } from './service/employees/employee.service';
import { PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { ConfirmActionDialogComponent, DialogResult } from './confirm-action-dialog-component/confirm-action-dialog-component.component';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'crudapp';
  displayedColumns = ['firstName', 'lastName', 'emailAddress', 'salary', 'actions'];
  pageIndex: number = 0;
  pageSize: number = 10;
  dataSource = new MatTableDataSource();
  totalItems: number = 0;
  filter: {
    email?: string;
    name?: string;
  } = {};
  constructor(private _dialog: MatDialog, private toastr: ToastrService, 
             private empService: EmployeeService) {

  }

  openAddEditEmployeeDialog() {
    const dialogRef = this._dialog.open(AddEditEmployeesComponent);
    dialogRef.afterClosed().subscribe({
      next: (val) => {
        this.loadEmployees();
      },
    });
  }


  async ngOnInit() {
    await this.loadEmployees();
  }

  async loadEmployees() {
    let name: string = this.filter.name!;
    let email: string = this.filter.email!;
    this.empService.getEmployees(name, email, this.pageSize, this.pageIndex)
      .subscribe((res) => {
        this.dataSource.data = (res.data as any).data;
        this.totalItems = (res.data as any).totalItems;
      });
  }

  pageChanged(pageChangedEvent: PageEvent) {
    this.pageSize = pageChangedEvent.pageSize;
    this.pageIndex = pageChangedEvent.pageIndex;
    this.loadEmployees();
  }

  openEditEmployeeDialog(data: any) {
    this._dialog.open(AddEditEmployeesComponent, { data });
  }

  confirmDeleteEmployee(empId: number) {
    const dialogRef = this._dialog.open(ConfirmActionDialogComponent,
      {
        data: { title: 'Confirm Delete', message: 'Are sure you want to delete the selected Employee?' }
      }
    );

    dialogRef.afterClosed().subscribe(result => {
      if (result == DialogResult.Yes) {
        this.deleteCategory(empId);
      }
    });
  }


  async deleteCategory(empId: number) {
    this.empService.deleteEmployee(empId).subscribe(data => {
      this.toastr.success('Employee deleted successfully');
      this.loadEmployees();
    }, error => {
      this.toastr.error(error);
    });
  }
}
