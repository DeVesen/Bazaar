import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

export interface IPageInfo {
  pageTitle: string;
  buttons?: ITitleActionBtn[]
}

export interface ITitleActionBtn {
  key: string;
  icon: string;
  class: string;
  style: string
}

@Injectable({
  providedIn: 'root'
})
export class ActivePageInfoService {

  private _pageTitleSubject$ = new BehaviorSubject<string>(null);
  public pageTitle$ = this._pageTitleSubject$.asObservable();


  constructor() { }


  public setPageTitel(pageTitle: string): void {
    this._pageTitleSubject$.next(pageTitle);
  }
}
