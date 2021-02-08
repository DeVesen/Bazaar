import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SimpleToolbarSearchInputComponent } from './simple-toolbar-search-input.component';

describe('SimpleToolbarSearchInputComponent', () => {
  let component: SimpleToolbarSearchInputComponent;
  let fixture: ComponentFixture<SimpleToolbarSearchInputComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SimpleToolbarSearchInputComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SimpleToolbarSearchInputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
