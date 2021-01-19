import { Component, Input, OnInit } from '@angular/core';
import { IArticle } from 'src/app/services/article-api-service/artikle-api.service';

@Component({
  selector: 'app-article-card-status-icons',
  templateUrl: './article-card-status-icons.component.html',
  styleUrls: ['./article-card-status-icons.component.scss']
})
export class ArticleCardStatusIconsComponent implements OnInit {
  
  @Input() article: IArticle;

  constructor() { }

  ngOnInit(): void {
  }

}
