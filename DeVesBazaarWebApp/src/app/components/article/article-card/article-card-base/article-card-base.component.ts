import { Component, Input, OnInit } from '@angular/core';
import { IArticle } from 'src/app/services/article-api-service/artikle-api.service';

@Component({
  selector: 'app-article-card-base',
  templateUrl: './article-card-base.component.html',
  styleUrls: ['./article-card-base.component.scss']
})
export class ArticleCardBaseComponent implements OnInit {

  @Input() article: IArticle;

  constructor() { }

  ngOnInit(): void {
  }
}
