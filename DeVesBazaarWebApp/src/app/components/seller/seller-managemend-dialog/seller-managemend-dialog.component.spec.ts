import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AngularMaterialModule } from 'src/app/components/angular-material/angular-material.module';

import { SellerManagemendDialogComponent } from './seller-managemend-dialog.component';

describe('SellerManagemendDialogComponent', () => {
  
  let component: SellerManagemendDialogComponent;
  let fixture: ComponentFixture<SellerManagemendDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [
        SellerManagemendDialogComponent
      ],
      imports: [
        AngularMaterialModule
      ],
      providers: [
        { provide: MatDialogRef, useValue: {} },
        { provide: MAT_DIALOG_DATA, useValue: null }
      ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SellerManagemendDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
