import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InputFieldDemoComponent } from './input-field-demo.component';

describe('InputFieldDemoComponent', () => {
  let component: InputFieldDemoComponent;
  let fixture: ComponentFixture<InputFieldDemoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InputFieldDemoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InputFieldDemoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
