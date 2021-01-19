import { OnChanges, SimpleChanges } from '@angular/core';
import { Component, Input, OnInit } from '@angular/core';
import { DateTimePipe } from 'src/app/pipes/date-time/date-time.pipe';
import { IArticle } from 'src/app/services/article-api-service/artikle-api.service';

@Component({
  selector: 'app-article-card-sale-content',
  templateUrl: './article-card-sale-content.component.html',
  styleUrls: ['./article-card-sale-content.component.scss']
})
export class ArticleCardSaleContentComponent implements OnInit, OnChanges {
  
  @Input() article: IArticle;

  priceValueText: string;

  constructor() { }

  ngOnInit(): void {
  }
  ngOnChanges(changes: SimpleChanges): void {

    this.priceValueText = this.article.price
                              ? `${this.article.price} €`
                              : '';
  }

}
