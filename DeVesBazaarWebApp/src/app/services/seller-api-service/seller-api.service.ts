import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';
import { GlobalHelper } from 'src/app/helpers/global-helper';
import { List } from 'src/app/helpers/list';
import { IResponse } from 'src/app/models/api-response-model';
import { ISeller } from 'src/app/models/seller-model';

@Injectable({
  providedIn: 'root'
})
export class SellerApiService {

  private _minSleep = 500;
  private _localList = new List<ISeller>();

  constructor() {
    this._localList.push({id: this.getEmptyId(), firstName: 'Andreas', lastName: 'Schmidt'});
    this._localList.push({id: this.getEmptyId(), firstName: 'Sven', lastName: 'Reichert'});
    this._localList.push({id: this.getEmptyId(), firstName: 'Michael', lastName: 'Zoller'});
    this._localList.push({id: this.getEmptyId(), firstName: 'Henrik', lastName: 'Schweder'});
  }


  public async getAll(): Promise<IResponse<ISeller[]>> {
    await GlobalHelper.sleep(this._minSleep);
    return {data: this._localList.all()};
  }


  public async idExist(id: number): Promise<IResponse<boolean>> {
    if (!id || id <= 0) {
      return GlobalHelper.createErrorResponse<boolean>('MsgUndefined');
    }
    await GlobalHelper.sleep(this._minSleep);
    return {data: this.existsById(id)};
  }

  public async create(element: ISeller): Promise<IResponse<ISeller>> {
    const elementValidation = this.validateForCreation(element);
    if (elementValidation) {
      return GlobalHelper.createErrorResponse<ISeller>(elementValidation);
    }

    await GlobalHelper.sleep(this._minSleep);

    element.id = element.id <= 0 ? this.getEmptyId() : element.id;
    
    if (this.existsById(element.id)) {
      return GlobalHelper.createErrorResponse<ISeller>('MsgIdAlreadyExist');
    }

    this._localList.push(element);

    return {data: element};
  }

  public async remove(elementId: number): Promise<IResponse<boolean>> {
    await GlobalHelper.sleep(this._minSleep);
    const removedEntries = this._localList.remove(p => p.id === elementId);
    return {data: removedEntries === 1};
  }


  public validateForCreation(element: ISeller): string {
    if (!element) {
      return 'MsgUndefined';
    }
    if (!element.lastName) {
      return 'MsgNameMissed';
    }
    return undefined;
  }

  public validateForUpdate(element: ISeller): string {
    if (!element) {
      return 'MsgUndefined';
    }
    if (element.id <= 0) {
      return 'MsgIdMissed';
    }
    if (!element.lastName) {
      return 'MsgNameMissed';
    }
    return undefined;
  }

  
  // Fake-Backend Helper-Functions:

  private existsById(id: number): boolean {
    return this._localList.exists(p => p.id === id);
  }

  private getEmptyId(): number {
    for(let index = 1; index < 10000000; index++) {
      if (!this._localList.exists(p => p.id === index)) {
        return index;
      }
    }
    throwError('No free ID found!');
  }
}
