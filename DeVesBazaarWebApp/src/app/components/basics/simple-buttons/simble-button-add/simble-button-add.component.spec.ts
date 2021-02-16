import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SimbleButtonAddComponent } from './simble-button-add.component';

describe('SimbleAddButtonComponent', () => {
  let component: SimbleButtonAddComponent;
  let fixture: ComponentFixture<SimbleButtonAddComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SimbleButtonAddComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SimbleButtonAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
