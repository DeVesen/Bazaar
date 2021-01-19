import { Component, Input, OnInit, SimpleChanges } from '@angular/core';
import { DateTimePipe } from 'src/app/pipes/date-time/date-time.pipe';
import { IArticle } from 'src/app/services/article-api-service/artikle-api.service';

@Component({
  selector: 'app-article-card-info-content',
  templateUrl: './article-card-info-content.component.html',
  styleUrls: ['./article-card-info-content.component.scss']
})
export class ArticleCardInfoContentComponent implements OnInit {
  
  @Input() article: IArticle;

  priceLableText: string;
  priceValueText: string;
  soldLableText: string;
  soldValueText: string;
  returnedLableText: string;
  returnedValueText: string;

  constructor() { }

  ngOnInit(): void {
  }
  ngOnChanges(changes: SimpleChanges): void {

    this.priceLableText = this.article.onSaleSince
                              ? `Preis (${DateTimePipe.toLocalDateTimeString(this.article.onSaleSince)})`
                              : 'Preis (nicht freigegeben)';
    this.priceValueText = this.article.price
                              ? `${this.article.price} €`
                              : '';
                              
    this.soldLableText = this.article.soldAt
                              ? `Verkauft (${DateTimePipe.toLocalDateTimeString(this.article.soldAt)})`
                              : 'Verkauft (nicht verkauft)';
    this.soldValueText = this.article.soldFor
                              ? `${this.article.soldFor} €`
                              : '';

    this.returnedLableText = this.article.soldAt && this.article.returnedAt
                              ? 'Abgerechnet'
                              : 'Zurück gegeben';
    this.returnedValueText = this.article.soldAt && this.article.returnedAt
                              ? DateTimePipe.toLocalDateTimeString(this.article.returnedAt)
                              : DateTimePipe.toLocalDateTimeString(this.article.returnedAt);
  }

}
