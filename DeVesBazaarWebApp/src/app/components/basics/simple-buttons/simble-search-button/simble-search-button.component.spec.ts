import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SimbleSearchButtonComponent } from './simble-search-button.component';

describe('SimbleSearchButtonComponent', () => {
  let component: SimbleSearchButtonComponent;
  let fixture: ComponentFixture<SimbleSearchButtonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SimbleSearchButtonComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SimbleSearchButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
