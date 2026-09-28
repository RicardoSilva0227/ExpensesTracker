import { Routes } from '@angular/router';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { ExpenseListComponent } from './components/expense/expense-list/expense-list.component';
import { ExpenseAddOrEditComponent } from './components/expense/expense-add-or-edit/expense-add-or-edit.component';
import { ExpenseTypeListComponent } from './components/expenseType/expense-type-list/expense-type-list.component';
import { ExpenseTypeAddOrEditComponent } from './components/expenseType/expense-type-add-or-edit/expense-type-add-or-edit.component';
import { ConfigsComponent } from './components/configs/configs.component'

export const routes: Routes = [
  // Redirects for lowercase
  { path: '', redirectTo: 'Dashboard', pathMatch: 'full' },
  { path: 'dashboard', redirectTo: 'Dashboard', pathMatch: 'full' },
  { path: 'expense/list', redirectTo: 'Expense/List', pathMatch: 'full' },
  { path: 'expense/addoredit', redirectTo: 'Expense/AddOrEdit', pathMatch: 'full' },
  { path: 'expensetype/list', redirectTo: 'ExpenseType/List', pathMatch: 'full' },
  { path: 'expensetype/addoredit', redirectTo: 'ExpenseType/AddOrEdit', pathMatch: 'full' },
  { path: 'configs', redirectTo: 'Configs', pathMatch: 'full' },

  // Actual routes
  { path: 'Dashboard', component: DashboardComponent },
  { path: 'Expense/List', component: ExpenseListComponent },
  { path: 'Expense/AddOrEdit', component: ExpenseAddOrEditComponent },
  { path: 'Expense/AddOrEdit/:id', component: ExpenseAddOrEditComponent },
  { path: 'ExpenseType/List', component: ExpenseTypeListComponent },
  { path: 'ExpenseType/AddOrEdit', component: ExpenseTypeAddOrEditComponent },
  { path: 'ExpenseType/AddOrEdit/:id', component: ExpenseTypeAddOrEditComponent },
  { path: 'Configs', component: ConfigsComponent },

  // Fallback
  { path: '**', redirectTo: 'Dashboard' }
];
  