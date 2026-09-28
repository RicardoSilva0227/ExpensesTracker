import { Component, OnInit } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { ApiService } from '../../../services/api.service';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';


@Component({
  selector: 'app-expense-type-list',
  imports: [MatTableModule, CommonModule],
  templateUrl: './expense-type-list.component.html',
  styleUrl: './expense-type-list.component.css'
})
export class ExpenseTypeListComponent {

  expenseTypes: any = [];
  pageSize: number = 10;
  pageNumber: number = 1;
  currentPage: number = 1;
  totalPages: number = 1;

  constructor(
    private apiService: ApiService,
    private router: Router,
    private snackBar: MatSnackBar,
  ) {}

  ngOnInit() {
    this.fetchExpenseTypes();
  }

  fetchExpenseTypes() {
    this.apiService.getAll('ExpensesTypes/GetAllExpenseTypes', this.pageSize, this.pageNumber).subscribe(response => {
      this.expenseTypes = response.result;
    });
  }


  
  SetEditExpenseType(id?:number){
    if(typeof id === 'undefined'){
      this.router.navigate(['/ExpenseType/AddOrEdit']);  
    } else {
      this.router.navigate(['/ExpenseType/AddOrEdit/', id]);  
    }
  }


  DeleteExpenseType(id?:number){
    if(id) {
       this.apiService.delete('ExpensesTypes/DeleteExpenseTypes', id).subscribe({
        next: (response) => {
          if (response.isSuccess) {
            this.snackBar.open('Expense type deleted successfully!', 'Close', {
              duration: 4000,
              panelClass: ['success-snackbar']
            });
          }
          this.fetchExpenseTypes();
        },
        error: (error) => {
          this.snackBar.open('Failed to delete expense type.', 'Close', {
            duration: 4000,
            panelClass: ['error-snackbar']
          });
        }
      });
    } else {
      this.snackBar.open('Id does not exist.', 'Close', {
            duration: 3000,
            panelClass: ['error-snackbar']
          });
    }
  }


  nextPage() {
    this.pageNumber++;
    this.fetchExpenseTypes();
  }

  prevPage() {
    if (this.pageNumber > 1) {
      this.pageNumber--;
      this.fetchExpenseTypes();
    }
  }
}
