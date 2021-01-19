import { Component, Input, OnInit } from '@angular/core';
import { IArticle } from '../../../../services/article-api-service/artikle-api.service';

@Component({
  selector: 'app-article-card-sale',
  templateUrl: './article-card-sale.component.html',
  styleUrls: ['./article-card-sale.component.scss']
})
export class ArticleCardSaleComponent implements OnInit {

  @Input() article: IArticle;

  constructor() { }

  ngOnInit(): void {
  }

}
