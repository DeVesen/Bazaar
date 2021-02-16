import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SimbleButtonSaveComponent } from './simble-button-save.component';

describe('SimbleSaveButtonComponent', () => {
  let component: SimbleButtonSaveComponent;
  let fixture: ComponentFixture<SimbleButtonSaveComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SimbleButtonSaveComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SimbleButtonSaveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
