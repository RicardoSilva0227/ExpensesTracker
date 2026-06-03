import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CurrencyModalComponent } from './Currencies/currency-modal.component';
import { ApiService } from '../../services/api.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { MatSelectModule } from '@angular/material/select';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { TIMEZONES, DATE_FORMATS, PAGINATION_SIZES, EXPORT_FORMATS } from '../../constants/config-options.constants';


@Component({
  selector: 'app-configs',
  imports: [CommonModule, ReactiveFormsModule, MatSelectModule, MatSlideToggleModule, CurrencyModalComponent],
  templateUrl: './configs.component.html',
  styleUrl: './configs.component.css'
})
export class ConfigsComponent implements OnInit {
  model: any;
  configsForm!: FormGroup; //the "!" tells angular to ignore if the class is already initialized or not
  private _id!: number;
  currencies: any[] = [];
  activeTab: string = 'ftp'
  showCurrencyModal = false;

  // enums
  timezones = TIMEZONES;
  dateFormats = DATE_FORMATS;
  paginationSizes = PAGINATION_SIZES;
  exportFormats = EXPORT_FORMATS;


  constructor(
    private fb: FormBuilder,
    private apiService: ApiService,
    private router: Router,
    private snackBar: MatSnackBar) {
  }

  ngOnInit() {

    this.initializeForm();
  }

  initializeForm() {
    this.apiService.getSingleton('Configs/GetConfig').subscribe({
      next: (response) => {
        if (response.isSuccess) {
          this.configsForm = this.fb.group({
            id: [response.result.id ?? null],
            // FTP
            useFtp: [response.result.useFtp ?? false],
            ftpServer: [response.result.ftpServer ?? ''],
            ftpUsername: [response.result.ftpUsername ?? ''],
            ftpPassword: [response.result.ftpPassword ?? ''],
            ftpPort: [response.result.ftpPort ?? 21],
            // SMTP
            useSmtp: [response.result.useSmtp ?? false],
            smtpServer: [response.result.smtpServer ?? ''],
            smtpPort: [response.result.smtpPort ?? null],
            smtpUsername: [response.result.smtpUsername ?? ''],
            smtpPassword: [response.result.smtpPassword ?? ''],
            // Folder
            useFolder: [response.result.useFolder ?? false],
            folderAddress: [response.result.folderAddress ?? ''],
            // System
            timezone: [response.result.timezone ?? 'UTC+1'],
            dateFormat: [response.result.dateFormat ?? 'dd/MM/yyyy'],
            enableMultiCurrency: [response.result.enableMultiCurrency ?? true],
            enableDiscounts: [response.result.enableDiscounts ?? false],
            defaultPaginationSize: [response.result.defaultPaginationSize ?? 25],
            defaultExportSetting: [response.result.defaultExportSetting ?? 1],
            // Currency
            defaultCurrencyId: [response.result.defaultCurrencyId ?? null]
          });

          // Set initial disabled state
          this.toggleFields(['ftpServer', 'ftpPort', 'ftpUsername', 'ftpPassword'], this.configsForm.value.useFtp);
          this.toggleFields(['smtpServer', 'smtpPort', 'smtpUsername', 'smtpPassword'], this.configsForm.value.useSmtp);
          this.toggleFields(['folderAddress'], this.configsForm.value.useFolder);

          // Watch for toggle changes
          this.configsForm.get('useFtp')?.valueChanges.subscribe(val =>
            this.toggleFields(['ftpServer', 'ftpPort', 'ftpUsername', 'ftpPassword'], val));

          this.configsForm.get('useSmtp')?.valueChanges.subscribe(val =>
            this.toggleFields(['smtpServer', 'smtpPort', 'smtpUsername', 'smtpPassword'], val));

          this.configsForm.get('useFolder')?.valueChanges.subscribe(val =>
            this.toggleFields(['folderAddress'], val));
        }
      },
      error: (error) => {
        this.snackBar.open('Failed to fetch config.', 'Close', {
          duration: 3000,
          panelClass: ['error-snackbar']
        });
      }
    });

    this.loadCurrencies();
  }


  loadCurrencies() {
    this.apiService.getAll('Configs/Currency/GetAllCurrencies').subscribe({
      next: (response) => {
        if (response.isSuccess) {
          this.currencies = response.result;
        }
      },
      error: (error) => {
        this.snackBar.open('Failed to fetch currencies.', 'Close', {
          duration: 3000,
          panelClass: ['error-snackbar']
        });
      }
    });
  }


  toggleFields(fields: string[], enable: boolean) {
    fields.forEach(field => {
      const control = this.configsForm.get(field);
      enable ? control?.enable() : control?.disable();
    });
  }

  onSubmit() {
    this.apiService.updateSingleton('Configs/UpdateConfig', this.configsForm.value).subscribe({
      next: (response) => {
        if (response.isSuccess) {
          this.snackBar.open('Configs updated successfully!', 'Close', {
            duration: 4000,
            panelClass: ['success-snackbar']
          });
          redirectTo: this.router.navigate(['/dashboard'])
        }
      },
      error: (error) => {
        this.snackBar.open('Failed to update Configs.', 'Close', {
          duration: 4000,
          panelClass: ['error-snackbar']
        });
      }
    });
  }

  onCurrencyCreated() {
    this.loadCurrencies();
    this.closeCurrencyModal();
  }

  openCurrencyModal() { this.showCurrencyModal = true; }
  closeCurrencyModal() { this.showCurrencyModal = false; }

}
