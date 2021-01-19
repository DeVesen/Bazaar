import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AngularMaterialModule } from '../../angular-material/angular-material.module';

import { SellerTableDetailRowComponent } from './seller-table-detail-row.component';

describe('SellerTableDetailRowComponent', () => {
  let component: SellerTableDetailRowComponent;
  let fixture: ComponentFixture<SellerTableDetailRowComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SellerTableDetailRowComponent ],
      imports: [
        AngularMaterialModule
      ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SellerTableDetailRowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('data is defined', () => {
    component.seller = {
      id: 1,
      salutation: 'Herr',
      firstName: 'f1',
      lastName: 'l1',
    };
    fixture.detectChanges();
    const compiled = fixture.nativeElement;
    expect(compiled.querySelector('.seller-element-detail-content').textContent).toContain('ÄndernArtikelLöschen');
  });

  it('data is no defined', () => {
    fixture.detectChanges();
    const compiled = fixture.nativeElement;
    expect(compiled.querySelector('.seller-element-detail-content').textContent).toContain('Input \'element\' is not set!');
  });
});
