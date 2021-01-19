import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AngularMaterialModule } from 'src/app/components/angular-material/angular-material.module';

import { SellerManagementPageComponent } from './seller-management-page.component';

describe('SellerManagementPageComponent', () => {
  let component: SellerManagementPageComponent;
  let fixture: ComponentFixture<SellerManagementPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SellerManagementPageComponent ],
      imports: [
        AngularMaterialModule
      ],
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SellerManagementPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should render add button', () => {
    fixture.detectChanges();
    const nativeElement = fixture.nativeElement;
    expect(nativeElement.querySelector('button .mat-icon').textContent).toBeTruthy();
  });
});
