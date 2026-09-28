import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExpenseTypeAddOrEditComponent } from './expense-type-add-or-edit.component';

describe('ExpenseTypeAddOrEditComponent', () => {
  let component: ExpenseTypeAddOrEditComponent;
  let fixture: ComponentFixture<ExpenseTypeAddOrEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ExpenseTypeAddOrEditComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ExpenseTypeAddOrEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
