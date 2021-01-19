import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AngularInfoPageComponent } from './angular-info-page.component';

describe('AngularInfoPageComponent', () => {
  let component: AngularInfoPageComponent;
  let fixture: ComponentFixture<AngularInfoPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AngularInfoPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AngularInfoPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
