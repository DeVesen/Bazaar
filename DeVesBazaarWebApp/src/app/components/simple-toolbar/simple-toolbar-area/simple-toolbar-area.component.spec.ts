import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SimpleToolbarAreaComponent } from './simple-toolbar-area.component';

describe('SimpleToolbarComponent', () => {
  let component: SimpleToolbarAreaComponent;
  let fixture: ComponentFixture<SimpleToolbarAreaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SimpleToolbarAreaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SimpleToolbarAreaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
