import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { EmployeeService } from '../../service/employees/employee.service';
import { Employee } from 'src/app/models';
import { DialogRef } from '@angular/cdk/dialog';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-add-edit-employees',
  templateUrl: './add-edit-employees.component.html',
  styleUrls: ['./add-edit-employees.component.css']
})
export class AddEditEmployeesComponent implements OnInit {
  employeeForm = this.fb.group({
    firstName: ['', [Validators.required, Validators.maxLength(500)]],
    lastName: ['', [Validators.required, Validators.maxLength(500)]],
    email: ['', [Validators.required, Validators.maxLength(500), Validators.email]],
    salary: ['', [Validators.required]],
  });

  employee: Partial<Employee> = {};

  constructor(private toastr: ToastrService, private fb: FormBuilder,
    private empService: EmployeeService,
    private dialogRef: MatDialogRef<AddEditEmployeesComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) {
  }

  ngOnInit(): void {
    this.employeeForm.patchValue(this.data);
    if (this.data)
      this.employee = this.data;
  }

  onSubmit() {

    if (this.employeeForm.invalid) {
      this.toastr.error("please fill all required fields!");
      return;
    }

    this.empService.saveEmployee(this.employee).subscribe({
      next: (val: any) => {
        this.toastr.success("Employee has been saved successfully !");
        this.dialogRef.close(true)
      }, error: (err: any) => {
        this.toastr.error("Error: when saving employee!");

      }
    });
  }

}
