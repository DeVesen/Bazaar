import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SimpleToolbarBtnComponent } from './simple-toolbar-btn.component';

describe('SimpleToolbarBtnComponent', () => {
  let component: SimpleToolbarBtnComponent;
  let fixture: ComponentFixture<SimpleToolbarBtnComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SimpleToolbarBtnComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SimpleToolbarBtnComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
