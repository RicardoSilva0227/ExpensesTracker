import { Component, OnInit } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { ApiService } from '../../../services/api.service';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { RedirectCommand } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';


@Component({
  selector: 'app-expense-list',
  imports: [MatTableModule, CommonModule],
  templateUrl: './expense-list.component.html',
  styleUrl: './expense-list.component.css'
})
export class ExpenseListComponent implements OnInit {
  expenses: any = [];
  pageSize: number = 10;
  pageNumber: number = 1;

  currentPage: number = 1;
  totalPages: number = 3;

  constructor(
    private apiService: ApiService,
    private router: Router,
    private snackBar: MatSnackBar,
  ) {}


  ngOnInit() {
    this.fetchExpenses();
  }

  fetchExpenses() {
    this.apiService.getAll('Expenses/GetAllExpenses', this.pageSize, this.pageNumber).subscribe(response => {
      this.expenses = response.result;
    });
  }
  
  SetEditExpense(id?:number){
    if(typeof id === 'undefined'){
      this.router.navigate(['/Expense/AddOrEdit']);  
    } else {
      this.router.navigate(['/Expense/AddOrEdit/', id]);  
    }
  }

  DeleteExpense(id?:number){
    if(id) {
       this.apiService.delete('Expenses/DeleteExpense', id).subscribe({
        next: (response) => {
          if (response.isSuccess) {
            this.snackBar.open('Expense deleted successfully!', 'Close', {
              duration: 4000,
              panelClass: ['success-snackbar']
            });

            this.fetchExpenses();
          }
        },
        error: (error) => {
          this.snackBar.open('Failed to delete expense.', 'Close', {
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
    this.fetchExpenses();
  }

  prevPage() {
    if (this.pageNumber > 1) {
      this.pageNumber--;
      this.fetchExpenses();
    }
  }
}
