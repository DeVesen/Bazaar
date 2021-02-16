import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SimpleTableButtonGroupComponent } from './simple-table-button-group.component';

describe('SimpleTableButtonGroupComponent', () => {
  let component: SimpleTableButtonGroupComponent;
  let fixture: ComponentFixture<SimpleTableButtonGroupComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SimpleTableButtonGroupComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SimpleTableButtonGroupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
