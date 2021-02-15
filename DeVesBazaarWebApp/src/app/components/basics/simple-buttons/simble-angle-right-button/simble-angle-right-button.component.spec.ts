import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SimbleAngleRightButtonComponent } from './simble-angle-right-button.component';

describe('SimbleAngleRightButtonComponent', () => {
  let component: SimbleAngleRightButtonComponent;
  let fixture: ComponentFixture<SimbleAngleRightButtonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SimbleAngleRightButtonComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SimbleAngleRightButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
