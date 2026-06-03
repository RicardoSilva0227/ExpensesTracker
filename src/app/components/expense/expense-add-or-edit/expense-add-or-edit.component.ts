import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule  } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ApiService } from '../../../services/api.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import {MatSelectModule} from '@angular/material/select';

@Component({
  selector: 'app-expense-add-or-edit',
  imports: [CommonModule, ReactiveFormsModule, MatSelectModule],
  templateUrl: './expense-add-or-edit.component.html',
  styleUrl: './expense-add-or-edit.component.css'
})

export class ExpenseAddOrEditComponent implements OnInit {
  model: any;
  expenseForm!: FormGroup; //the "!" tells angular to ignore if the class is already initialized or not
  expenseTypes: any[] = [];
  isEditing: boolean = false;

  private _id!: number;

  @Input()
  set id(expenseId: number) {
    this._id = expenseId;
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
      this.apiService.getById('Expenses/GetExpense', this._id).subscribe({
        next: (response) => {
          if (response.isSuccess){

            this.expenseForm =  this.fb.group({
              Id: [response.result.id],
              Code: [response.result.code],
              Title: [response.result.title, [Validators.required, Validators.minLength(3)]],
              Amount: [response.result.amount, [Validators.required, Validators.min(1)]],
              Tin: [response.result.tin, [Validators.minLength(9), Validators.maxLength(9)]],
              DateOfEmission: [this.formatDateForInput(response.result.dateOfEmission), [Validators.required]],
              ExpenseTypeId: [response.result.expenseTypeId, Validators.required]
            });
            
          }
        },
        error: (error) => {
          this.snackBar.open('Failed to fetch expenses.', 'Close', {
            duration: 3000,
            panelClass: ['error-snackbar']
          });
        }
      });
    } else {
      this.expenseForm = this.fb.group({
        Code: [''],
        Title: ['', [Validators.required, Validators.minLength(3)]],
        Amount: ['', [Validators.required, Validators.min(1)]],
        Tin: ['', [Validators.minLength(9), Validators.maxLength(9)]],
        DateOfEmission: ['', [Validators.required]],
        ExpenseTypeId: ['', Validators.required],
      });
    }

    this.loadExpenseType();
  }

  loadExpenseType(){
    this.apiService.getAll('ExpensesTypes/GetAllExpenseTypes').subscribe({
      next: (response) => {
        if (response.isSuccess){
          this.expenseTypes = response.result;
        }
      },
      error: (error) => {
        this.snackBar.open('Failed to fetch expenses.', 'Close', {
          duration: 3000,
          panelClass: ['error-snackbar']
        });
      }
    });
  }

  // remove the hours part of the date
  private formatDateForInput(dateString: string | null): string {
    if (!dateString) return '';
    return new Date(dateString).toISOString().split('T')[0];
  }

  goBack(){
    this.router.navigate(['Expense/List']);
  }

  onSubmit() {
  const formValue = this.expenseForm.value;

  const payload = {
    ...(this._id && { Id: formValue.Id }),
    Code: formValue.Code,
    Title: formValue.Title,
    Amount: formValue.Amount,
    Tin: formValue.Tin ? +formValue.Tin : null,
    DateOfEmission: formValue.DateOfEmission,
    ExpenseTypeId: +formValue.ExpenseTypeId
  }

    if (this._id){
      this.apiService.update('Expenses/UpdateExpense', this._id, payload).subscribe({
        next: (response) => {
          if (response.isSuccess) {
            this.snackBar.open('Expense updated successfully!', 'Close', {
              duration: 4000,
              panelClass: ['success-snackbar']
            });
          }
        
          this.goBack();
        },
        error: (error) => {
          this.snackBar.open('Failed to update expense.', 'Close', {
            duration: 4000,
            panelClass: ['error-snackbar']
          });
        }
      });
    } else {
      this.apiService.create('Expenses/CreateExpense', payload).subscribe({
        next: (response) => {
          if (response.isSuccess) {
            this.snackBar.open('Expense added successfully!', 'Close', {
              duration: 4000,
              panelClass: ['success-snackbar']
            });
          }
        },
        error: (error) => {
          this.snackBar.open('Failed to add expense.', 'Close', {
            duration: 4000,
            panelClass: ['error-snackbar']
          });
        }
      });
    }
  }
}