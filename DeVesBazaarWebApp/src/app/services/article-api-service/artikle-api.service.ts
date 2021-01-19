import { Injectable } from '@angular/core';
import { of, Subject } from 'rxjs';
import { MemoryList } from '../memory-list/memory-list';


export interface IArticle {
  id: number;
  sellerNumber: number;

  title: string;
  category: string;
  manufacturer: string;

  onSaleSince?: Date;
  price: number;
  
  soldAt?: Date;
  soldFor?: number;
  
  returnedAt?: Date;
}

const ARTICLE_ELEMENT_DATA: IArticle[] = [
  {
    id: 1,
    sellerNumber: 1,
    title: 'Ski-Helm',
    category: 'Helm',
    manufacturer: 'HEAD',
    price: 10,
    returnedAt: new Date()
  },
  {
    id: 2,
    sellerNumber: 1,
    title: 'Ski-Handschuhe',
    category: 'Handschuhe',
    manufacturer: 'HEAD',
    price: 10,
    onSaleSince: new Date()
  },
  {
    id: 3,
    sellerNumber: 1,
    title: 'Ski',
    category: 'Ski',
    manufacturer: 'HEAD',
    price: 25,
    onSaleSince: new Date(),
    soldAt: new Date(),
    soldFor: 25
  },
  {
    id: 4,
    sellerNumber: 1,
    title: 'Ski-Stöcke Lekei',
    category: 'Stöcke',
    manufacturer: 'Lekei',
    price: 45,
    onSaleSince: new Date(),
    soldAt: new Date(),
    soldFor: 45
  }
];


@Injectable({
  providedIn: 'root'
})
export class ArtikleApiService {

  private _innerLst = new MemoryList<IArticle>(ARTICLE_ELEMENT_DATA, {
    getItemById: (lst: IArticle[], id: number) => {
      return lst.find(p => p.id === id);
    },
    getItemByRef: (lst: IArticle[], ref: IArticle) => {
      return lst.find(p => p.id === ref.id);
    },
    updateItem: (lstElem: IArticle, source: IArticle) => {
      lstElem.id = source.id;
      lstElem.sellerNumber = source.sellerNumber;
      lstElem.title = source.title;
      lstElem.category = source.category;
      lstElem.manufacturer = source.manufacturer;
      lstElem.onSaleSince = source.onSaleSince;
      lstElem.price = source.price;
      lstElem.soldAt = source.soldAt;
      lstElem.soldFor = source.soldFor;
      lstElem.returnedAt = source.returnedAt;
    }
  });

  private _dataUpdated$ = new Subject<number>();
  public dataUpdated = this._dataUpdated$.asObservable();

  constructor() { }


  public async getAll(sellerId?: number): Promise<IArticle[]> {
    const articles = sellerId && sellerId > 0
      ? this._innerLst.getAll().filter(p => p.sellerNumber === sellerId)
      : this._innerLst.getAll();
    return of(articles).toPromise();
  }

  public async get(articleId: number): Promise<IArticle> {
    return of(this._innerLst.get(articleId)).toPromise();
  }
}
