import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SimpleTableButtonComponent } from './simple-table-button.component';

describe('SimpleTableButtonComponent', () => {
  let component: SimpleTableButtonComponent;
  let fixture: ComponentFixture<SimpleTableButtonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SimpleTableButtonComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SimpleTableButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
