import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SimbleButtonSearchComponent } from './simble-button-search.component';

describe('SimbleSearchButtonComponent', () => {
  let component: SimbleButtonSearchComponent;
  let fixture: ComponentFixture<SimbleButtonSearchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SimbleButtonSearchComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SimbleButtonSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
