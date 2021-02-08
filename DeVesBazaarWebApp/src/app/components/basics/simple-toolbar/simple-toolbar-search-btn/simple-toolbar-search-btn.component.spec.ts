import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SimpleToolbarSearchBtnComponent } from './simple-toolbar-search-btn.component';

describe('SimpleToolbarSearchBtnComponent', () => {
  let component: SimpleToolbarSearchBtnComponent;
  let fixture: ComponentFixture<SimpleToolbarSearchBtnComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SimpleToolbarSearchBtnComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SimpleToolbarSearchBtnComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
