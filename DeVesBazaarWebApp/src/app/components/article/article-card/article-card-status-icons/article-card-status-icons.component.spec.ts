import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ArticleCardStatusIconsComponent } from './article-card-status-icons.component';

describe('ArticleCardStatusIconsComponent', () => {
  let component: ArticleCardStatusIconsComponent;
  let fixture: ComponentFixture<ArticleCardStatusIconsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ArticleCardStatusIconsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ArticleCardStatusIconsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
