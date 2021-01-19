import { Component, OnInit } from '@angular/core';
import { IArticle } from '../../../services/article-api-service/artikle-api.service';

@Component({
  selector: 'app-article-card-demo',
  templateUrl: './article-card-demo.component.html',
  styleUrls: ['./article-card-demo.component.scss']
})
export class ArticleCardDemoComponent implements OnInit {

  public articles: IArticle[];


  constructor() { }

  ngOnInit(): void {
    const actualDate = new Date();

    this.articles = [
      {
        id: 4711,
        sellerNumber: 22,
      
        title: 'Nicht im Verkauf',
        category: 'SKI',
        manufacturer: 'HEAD',
        price: 22.5
      },
      {
        id: 4711,
        sellerNumber: 22,
      
        title: 'Im Verkauf',
        category: 'SKI',
        manufacturer: 'HEAD',
        price: 22.5,

        onSaleSince: actualDate
      },
      {
        id: 4711,
        sellerNumber: 22,
      
        title: 'Verkauft',
        category: 'SKI',
        manufacturer: 'HEAD',
        price: 22.5,

        onSaleSince: actualDate,
        soldAt: actualDate,
        soldFor: 22.5,
      },
      {
        id: 4711,
        sellerNumber: 22,
      
        title: 'Zurückgegeben',
        category: 'SKI',
        manufacturer: 'HEAD',
        price: 22.5,

        returnedAt: actualDate
      },
      {
        id: 4711,
        sellerNumber: 22,
      
        title: 'Im Verkauf aber zurückgegeben',
        category: 'SKI',
        manufacturer: 'HEAD',
        price: 22.5,

        onSaleSince: actualDate,
        returnedAt: actualDate
      },
      {
        id: 4711,
        sellerNumber: 22,
      
        title: 'Verkauft und abgerechnet',
        category: 'SKI',
        manufacturer: 'HEAD',
        price: 22.5,

        onSaleSince: actualDate,
        soldAt: actualDate,
        soldFor: 22.5,
        returnedAt: actualDate
      }
    ];
  }

}
