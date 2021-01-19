import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ArticleCardBaseComponent } from './article-card-base.component';

describe('ArticleCardBaseComponent', () => {
  let component: ArticleCardBaseComponent;
  let fixture: ComponentFixture<ArticleCardBaseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ArticleCardBaseComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ArticleCardBaseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
