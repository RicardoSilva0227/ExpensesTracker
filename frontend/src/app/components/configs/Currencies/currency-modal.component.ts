import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ApiService } from '../../../services/api.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
    selector: 'app-currency-form',
    standalone: true,
    imports: [CommonModule, ReactiveFormsModule],
    templateUrl: './currency-modal.component.html',
})
export class CurrencyModalComponent implements OnInit {

    @Output() currencyCreated = new EventEmitter<void>();
    @Output() close = new EventEmitter<void>();

    currencyForm!: FormGroup;

    constructor(
        private fb: FormBuilder,
        private apiService: ApiService,
        private snackBar: MatSnackBar
    ) { }

    ngOnInit() {
        this.currencyForm = this.fb.group({
            name: ['', [Validators.required, Validators.minLength(2)]],
            acronym: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(5)]],
            symbol: ['', [Validators.required, Validators.maxLength(5)]],
            decimalPlaces: [0, [Validators.required, Validators.min(0), Validators.max(8)]],
            cultureCode: ['', [Validators.required]],
            country: [null],
            exchangeRateToBase: [null],
            isCrypto: [false]
        });
    }

    onSubmit() {
        if (this.currencyForm.invalid) return;

        this.apiService.create('Configs/Currency/CreateCurrency', this.currencyForm.value).subscribe({
            next: (response) => {
                if (response.isSuccess) {
                    this.snackBar.open('Currency created successfully!', 'Close', {
                        duration: 3000,
                        panelClass: ['success-snackbar']
                    });
                    this.currencyCreated.emit();
                    this.close.emit();
                }
            },
            error: () => {
                this.snackBar.open('Failed to create currency.', 'Close', {
                    duration: 3000,
                    panelClass: ['error-snackbar']
                });
            }
        });
    }
}