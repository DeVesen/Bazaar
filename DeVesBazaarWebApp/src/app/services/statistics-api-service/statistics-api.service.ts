import { Injectable } from '@angular/core';
import { ISellerStatistic } from 'src/app/models/seller-statisic';
import { ArtikleApiService, IArticle } from '../article-api-service/artikle-api.service';

@Injectable({
  providedIn: 'root'
})
export class StatisticsApi {

  constructor(private _articleApi: ArtikleApiService) { }


  public async get(sellerId?: number): Promise<ISellerStatistic> {
    const articles = (await this._articleApi.getAll(sellerId)) ?? [];

    const onSaleSinceArticles = articles.filter(x => this.onSale(x)) ?? [];
    const soldArticles = articles.filter(x => this.sold(x)) ?? [];
    const returnedArticles = articles.filter(x => x.returnedAt ? true : false) ?? [];

    const sales = soldArticles.reduce((sum, current) => sum + current.soldFor, 0);
    const tax = sales * 15 / 100;

    const result: ISellerStatistic = {
      sellerId: sellerId ?? 0,

      articles: articles.length,
      onSold: onSaleSinceArticles.length,
      sold: soldArticles.length,
      returned: returnedArticles.length,

      sales: sales,
      tax: tax,
      net: sales - tax
    };

    // console.log('ArticleStatisticsApi', { items: articles.length, result: result});

    return result;
  }


  private onSale(article: IArticle): boolean {
    return article && article.onSaleSince && !this.sold(article) && !this.returned(article)
      ? true
      : false;
  }
  private sold(article: IArticle): boolean {
    return article && article.soldAt && article.soldFor
      ? true
      : false;
  }
  private returned(article: IArticle): boolean {
    return article && article.returnedAt
      ? true
      : false;
  }
}
