import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ArticleCardDemoComponent } from './article-card-demo.component';

describe('ArticleCardDemoComponent', () => {
  let component: ArticleCardDemoComponent;
  let fixture: ComponentFixture<ArticleCardDemoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ArticleCardDemoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ArticleCardDemoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
