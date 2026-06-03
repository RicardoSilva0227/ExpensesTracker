import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule  } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ApiService } from '../../../services/api.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import {MatSelectModule} from '@angular/material/select';

@Component({
  selector: 'app-expense-type-add-or-edit',
  imports: [CommonModule, ReactiveFormsModule, MatSelectModule],
  templateUrl: './expense-type-add-or-edit.component.html',
  styleUrl: './expense-type-add-or-edit.component.css'
})
export class ExpenseTypeAddOrEditComponent {
  model: any;
  expenseTypesForm!: FormGroup; //the "!" tells angular to ignore if the class is already initialized or not
  isEditing: boolean = false;
  private _id!: number;

  @Input()
  set id(expenseTypeId: number) {
    this._id = expenseTypeId;
  }

  constructor(
    private fb: FormBuilder,
    private apiService: ApiService,
    private router: Router,
    private snackBar: MatSnackBar) {      
    }

  ngOnInit() {
    this.initializeForm();
  }

  initializeForm(){
    if (this._id) {
      this.isEditing = true;
      this.apiService.getById('ExpensesTypes/GetExpenseType', this._id).subscribe({
        next: (response) => {
          if (response.isSuccess){
            this.expenseTypesForm =  this.fb.group({
              id: [response.result.id],
              code: [response.result.code, [Validators.required, Validators.minLength(3)]],
              description: [response.result.description],
              icon: [response.result.icon]
            });
            
          }
        },
        error: (error) => {
          this.snackBar.open('Failed to fetch expense type.', 'Close', {
            duration: 3000,
            panelClass: ['error-snackbar']
          });
        }
      });
    } else {
      this.expenseTypesForm = this.fb.group({
        code: ['', [Validators.required, Validators.minLength(3)]],
        description: [''],
        icon: ['']
      });
    }
  }

  goBack(){
    this.router.navigate(['ExpenseType/List']);
  }

  onSubmit(model:any) {
    if (this._id){
      this.apiService.update('ExpensesTypes/UpdateExpenseType', this._id, model).subscribe({
        next: (response) => {
          if (response.isSuccess) {
            this.snackBar.open('Expense type updated successfully!', 'Close', {
              duration: 4000,
              panelClass: ['success-snackbar']
            });

            this.goBack();
          }
        },
        error: (error) => {
          this.snackBar.open('Failed to update expense.', 'Close', {
            duration: 4000,
            panelClass: ['error-snackbar']
          });
        }
      });
    } else {
      this.apiService.create('ExpensesTypes/CreateExpenseType', model).subscribe({
        next: (response) => {
          if (response.isSuccess) {
            this.snackBar.open('Expense type added successfully!', 'Close', {
              duration: 4000,
              panelClass: ['success-snackbar']
            });
          }

          this.goBack();
        },
        error: (error) => {
          this.snackBar.open('Failed to add expense type.', 'Close', {
            duration: 4000,
            panelClass: ['error-snackbar']
          });
        }
      });
    }
  }
}
