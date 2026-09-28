import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApiService } from '../../services/api.service';
import { MatSnackBar } from '@angular/material/snack-bar';


@Component({
  selector: 'app-import-file-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.css'],
  imports: [CommonModule],
})



export class ModalComponent {

  constructor(
    private apiService: ApiService,
    private snackBar: MatSnackBar) {
  }

  selectedFile: File | null = null;
  importStatus: string = 'No file selected';

  onFileSelected(event: Event): void {
    const target = event.target as HTMLInputElement;
    if (target.files && target.files.length > 0) {
      this.selectedFile = target.files[0];

      if (this.selectedFile.type !== 'application/pdf') {
        this.importStatus = 'Only PDF files are allowed.';
        this.selectedFile = null;
      } else {
        this.importStatus = "";
      }
    }
  }

  importFile(): void {

    if (!this.selectedFile || this.selectedFile && this.selectedFile.type !== 'application/pdf') {
      this.importStatus = 'Please select a PDF file to import.';
    } else if (this.selectedFile && this.selectedFile.type == 'application/pdf') {
      this.apiService.import('Expenses/ImportExpenses', this.selectedFile).subscribe({
        next: (response) => {
          if (response.isSuccess) {
            this.snackBar.open('Expenses imported successfully!', 'Close', {
              duration: 4000,
              panelClass: ['success-snackbar']
            });
          }
        },
        error: (error) => {
          this.snackBar.open('Failed to import expenses.', 'Close', {
            duration: 4000,
            panelClass: ['error-snackbar']
          });
        }
      });
    }

  }
}
