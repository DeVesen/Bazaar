import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SimpleToolbarRefreshBtnComponent } from './simple-toolbar-refresh-btn.component';

describe('SimpleToolbarRefreshBtnComponent', () => {
  let component: SimpleToolbarRefreshBtnComponent;
  let fixture: ComponentFixture<SimpleToolbarRefreshBtnComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SimpleToolbarRefreshBtnComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SimpleToolbarRefreshBtnComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
