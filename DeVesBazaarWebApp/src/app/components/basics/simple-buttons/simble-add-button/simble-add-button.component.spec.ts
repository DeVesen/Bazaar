import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SimbleAddButtonComponent } from './simble-add-button.component';

describe('SimbleAddButtonComponent', () => {
  let component: SimbleAddButtonComponent;
  let fixture: ComponentFixture<SimbleAddButtonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SimbleAddButtonComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SimbleAddButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
