import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManufacturerCreateDialogComponent } from './manufacturer-create-dialog.component';

describe('ManufacturerCreateDialogComponent', () => {
  let component: ManufacturerCreateDialogComponent;
  let fixture: ComponentFixture<ManufacturerCreateDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ManufacturerCreateDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ManufacturerCreateDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
