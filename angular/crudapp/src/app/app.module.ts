import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component'; 
import { AddEditEmployeesComponent } from './employee/add-edit-employees/add-edit-employees.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { HttpClientModule } from '@angular/common/http';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { ConfirmActionDialogComponent } from './confirm-action-dialog-component/confirm-action-dialog-component.component';

@NgModule({
  declarations: [
    AppComponent, 
    AddEditEmployeesComponent, ConfirmActionDialogComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatToolbarModule,MatDividerModule,
    MatIconModule, MatButtonModule, MatDialogModule,
    MatFormFieldModule, MatInputModule, FormsModule, ReactiveFormsModule,
    HttpClientModule,MatTableModule,MatPaginatorModule,MatCardModule,
    ToastrModule.forRoot({
      
    }),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
