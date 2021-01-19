import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ArticleCardInfoContentComponent } from './article-card-info-content.component';

describe('ArticleCardInfoContentComponent', () => {
  let component: ArticleCardInfoContentComponent;
  let fixture: ComponentFixture<ArticleCardInfoContentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ArticleCardInfoContentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ArticleCardInfoContentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
