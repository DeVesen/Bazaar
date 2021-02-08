import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SellerCreateDialogComponent } from './seller-create-dialog.component';

describe('SellerCreateDialogComponent', () => {
  let component: SellerCreateDialogComponent;
  let fixture: ComponentFixture<SellerCreateDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SellerCreateDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SellerCreateDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
