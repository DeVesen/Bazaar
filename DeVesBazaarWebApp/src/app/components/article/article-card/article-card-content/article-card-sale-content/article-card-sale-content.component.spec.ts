import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ArticleCardSaleContentComponent } from './article-card-sale-content.component';

describe('ArticleCardSaleContentComponent', () => {
  let component: ArticleCardSaleContentComponent;
  let fixture: ComponentFixture<ArticleCardSaleContentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ArticleCardSaleContentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ArticleCardSaleContentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
