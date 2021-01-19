import { Injectable } from '@angular/core';
import { of, Subject } from 'rxjs';
import { ISellerStatistic } from 'src/app/models/seller-statisic';
import { ISeller } from '../../models/seller';
import { MemoryList } from '../memory-list/memory-list';


const SELLER_ELEMENT_DATA: ISeller[] = [
  {id: 1, salutation: 'Herr', firstName: 'Town', lastName: 'Lazy', street: 'Lazy-Street 5b', zip: '12345', town: "Lazy-Town", phone: '4711', eMail: '1@1-cpm'},
  {id: 2, salutation: 'Herr', firstName: 'Hans', lastName: 'Dampf', street: 'Obere Straße 11a', eMail: '1@1-cpm'},
  {id: 3, salutation: 'Herr', firstName: 'Michael', lastName: 'Meyer', town: "KA", eMail: '2@2-cpm'},
  {id: 4, salutation: 'Herr', firstName: 'Egon', lastName: 'Baldes', phone: '4711', eMail: '3@4-cpm'},
  {id: 5, salutation: 'Herr', firstName: 'Otto', lastName: 'Baldes', eMail: '4@1-cpm'}
];

const STATISTIC_ELEMENT_DATA: ISellerStatistic[] = [
  {
    sellerId: 1,
    articles: 10,
    onSold: 8,
    sold: 3,
    returned: 0,
    sales: 100,
    tax: 15,
    net: 85
  }
];


@Injectable({
  providedIn: 'root'
})
export class SellerApiService {

  private _innerLst = new MemoryList<ISeller>(SELLER_ELEMENT_DATA, {
    getItemById: (lst: ISeller[], id: number) => {
      return lst.find(p => p.id === id);
    },
    getItemByRef: (lst: ISeller[], ref: ISeller) => {
      return lst.find(p => p.id === ref.id);
    },
    updateItem: (lstElem: ISeller, source: ISeller) => {
      lstElem.salutation = source.salutation;
      lstElem.firstName = source.firstName;
      lstElem.lastName = source.lastName;
      lstElem.street = source.street;
      lstElem.zip = source.zip;
      lstElem.town = source.town;
      lstElem.phone = source.phone;
      lstElem.eMail = source.eMail;
    }
  });

  private _dataUpdated$ = new Subject<number>();
  public dataUpdated = this._dataUpdated$.asObservable();

  constructor() { }


  public async getAll(): Promise<ISeller[]> {
    return of(this._innerLst.getAll()).toPromise();
  }

  public async get(number: number): Promise<ISeller> {
    return of(this._innerLst.get(number)).toPromise();
  }


  public async create(data: ISeller): Promise<any> {
    console.log('SellerService::create', data);
    this._innerLst.add(data);
    return of().toPromise()
               .then(() => this._dataUpdated$.next(data.id));
  }

  public async update(data: ISeller): Promise<any> {
    console.log('SellerService::update', data);
    this._innerLst.update(data);
    return of().toPromise()
               .then(() => this._dataUpdated$.next(data.id));
  }

  public async remove(number: number): Promise<any> {
    console.log('SellerService::remove', number);
    this._innerLst.remove(number);
    return of().toPromise()
               .then(() => this._dataUpdated$.next(number));
  }


  public async idExist(number: number): Promise<boolean> {
    return (await this.get(number)) ? true : false;
  }
}
