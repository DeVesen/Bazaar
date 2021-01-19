import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PageTitleComponent } from './page-title.component';
import { AngularMaterialModule } from 'src/app/components/angular-material/angular-material.module';

describe('PageTitleComponent', () => {
  let component: PageTitleComponent;
  let fixture: ComponentFixture<PageTitleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PageTitleComponent ],
      imports: [
        AngularMaterialModule
      ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PageTitleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should render title #1', () => {
    component.title = 'Hello-World';
    fixture.detectChanges();
    const nativeElement = fixture.nativeElement;
    expect(nativeElement.querySelector('.toolbar-title').textContent).toContain('Hello-World');
  });

  it('should render title #2', () => {
    const actualDate = ("0" + new Date().getDate()).slice(-2);
    component.title = actualDate;
    fixture.detectChanges();
    const nativeElement = fixture.nativeElement;
    expect(nativeElement.querySelector('.toolbar-title').textContent).toContain(actualDate);
  });
});
