import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SimbleRefreshButtonComponent } from './simble-refresh-button.component';

describe('SimbleRefreshButtonComponent', () => {
  let component: SimbleRefreshButtonComponent;
  let fixture: ComponentFixture<SimbleRefreshButtonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SimbleRefreshButtonComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SimbleRefreshButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
