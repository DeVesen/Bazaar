import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SimbleButtonAngleRightComponent } from './simble-button-angle-right.component';

describe('SimbleAngleRightButtonComponent', () => {
  let component: SimbleButtonAngleRightComponent;
  let fixture: ComponentFixture<SimbleButtonAngleRightComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SimbleButtonAngleRightComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SimbleButtonAngleRightComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
