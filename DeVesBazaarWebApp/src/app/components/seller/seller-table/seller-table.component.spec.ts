import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SellerTableComponent } from './seller-table.component';

describe('SellerTableComponent', () => {
  let component: SellerTableComponent;
  let fixture: ComponentFixture<SellerTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SellerTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SellerTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
