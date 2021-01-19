import { Component, Input, OnInit } from '@angular/core';
import { IArticle } from 'src/app/services/article-api-service/artikle-api.service';

@Component({
  selector: 'app-article-card-info',
  templateUrl: './article-card-info.component.html',
  styleUrls: ['./article-card-info.component.scss']
})
export class ArticleCardInfoComponent implements OnInit {

  @Input() article: IArticle;

  constructor() { }

  ngOnInit(): void {
  }

}
