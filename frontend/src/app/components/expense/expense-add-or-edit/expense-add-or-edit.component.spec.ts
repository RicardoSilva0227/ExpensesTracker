import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExpenseAddOrEditComponent } from './expense-add-or-edit.component';

describe('ExpenseAddOrEditComponent', () => {
  let component: ExpenseAddOrEditComponent;
  let fixture: ComponentFixture<ExpenseAddOrEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ExpenseAddOrEditComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ExpenseAddOrEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
