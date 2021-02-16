import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SimbleButtonRefreshComponent } from './simble-button-refresh.component';

describe('SimbleRefreshButtonComponent', () => {
  let component: SimbleButtonRefreshComponent;
  let fixture: ComponentFixture<SimbleButtonRefreshComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SimbleButtonRefreshComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SimbleButtonRefreshComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
