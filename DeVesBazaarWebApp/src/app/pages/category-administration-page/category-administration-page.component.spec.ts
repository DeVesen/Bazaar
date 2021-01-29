import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CategoryAdministrationPageComponent } from './category-administration-page.component';

describe('CategoryAdministrationPageComponent', () => {
  let component: CategoryAdministrationPageComponent;
  let fixture: ComponentFixture<CategoryAdministrationPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CategoryAdministrationPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CategoryAdministrationPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
