import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ArticleCardSaleComponent } from './article-card-sale.component';

describe('ArticleCardSaleComponent', () => {
  let component: ArticleCardSaleComponent;
  let fixture: ComponentFixture<ArticleCardSaleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ArticleCardSaleComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ArticleCardSaleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
