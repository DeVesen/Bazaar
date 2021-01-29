import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManufacturerAdministrationPageComponent } from './manufacturer-administration-page.component';

describe('ManufacturerAdministrationPageComponent', () => {
  let component: ManufacturerAdministrationPageComponent;
  let fixture: ComponentFixture<ManufacturerAdministrationPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ManufacturerAdministrationPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ManufacturerAdministrationPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
