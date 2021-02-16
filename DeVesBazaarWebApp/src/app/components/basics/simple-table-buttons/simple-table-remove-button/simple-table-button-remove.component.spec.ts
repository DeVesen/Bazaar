import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SimpleTableButtonRemoveComponent } from './simple-table-button-remove.component';

describe('SimpleRemoveTableButtonComponent', () => {
  let component: SimpleTableButtonRemoveComponent;
  let fixture: ComponentFixture<SimpleTableButtonRemoveComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SimpleTableButtonRemoveComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SimpleTableButtonRemoveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
