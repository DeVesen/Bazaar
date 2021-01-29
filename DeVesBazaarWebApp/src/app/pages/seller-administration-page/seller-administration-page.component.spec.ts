import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SellerAdministrationPageComponent } from './seller-administration-page.component';

describe('SellerAdministrationPageComponent', () => {
  let component: SellerAdministrationPageComponent;
  let fixture: ComponentFixture<SellerAdministrationPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SellerAdministrationPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SellerAdministrationPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
