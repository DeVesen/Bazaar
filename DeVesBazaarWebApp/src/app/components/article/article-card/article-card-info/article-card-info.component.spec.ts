import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ArticleCardInfoComponent } from './article-card-info.component';

describe('ArticleCardInfoComponent', () => {
  let component: ArticleCardInfoComponent;
  let fixture: ComponentFixture<ArticleCardInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ArticleCardInfoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ArticleCardInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
