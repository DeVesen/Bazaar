import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SimpleToolbarNewBtnComponent } from './simple-toolbar-new-btn.component';

describe('SimpleToolbarNewBtnComponent', () => {
  let component: SimpleToolbarNewBtnComponent;
  let fixture: ComponentFixture<SimpleToolbarNewBtnComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SimpleToolbarNewBtnComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SimpleToolbarNewBtnComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
