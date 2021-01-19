import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SellerSelectionInputDemoComponent } from './seller-selection-input-demo.component';

describe('SellerSelectionInputDemoComponent', () => {
  let component: SellerSelectionInputDemoComponent;
  let fixture: ComponentFixture<SellerSelectionInputDemoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SellerSelectionInputDemoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SellerSelectionInputDemoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
