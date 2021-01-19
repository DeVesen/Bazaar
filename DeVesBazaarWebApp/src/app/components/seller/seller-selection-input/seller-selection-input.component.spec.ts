import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SellerSelectionInputComponent } from './seller-selection-input.component';

describe('SellerSelectionInputComponent', () => {
  let component: SellerSelectionInputComponent;
  let fixture: ComponentFixture<SellerSelectionInputComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SellerSelectionInputComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SellerSelectionInputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
