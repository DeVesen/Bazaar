import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';
import { IResponse } from 'src/app/models/api-response-model';
import { ICategory } from '../../models/category-model';
import { GlobalHelper } from '../helpers/global-helper';
import { List } from '../helpers/list';

@Injectable({
  providedIn: 'root'
})
export class CategoryApiService {

  private _minSleep = 500;
  private _categories = new List<ICategory>();

  constructor() {
    [
      'Helm',
      'Ski',
      'Brille',
      'Hose',
      'Schuhe',
      'Ski-Schuhe',
      'Snowboard-Schuhe',
      'Stöcke'
    ].forEach(v => this._categories.push({id: this.getEmptyId(), name: v}));

  }


  public async getAll(): Promise<IResponse<ICategory[]>> {
    await GlobalHelper.sleep(this._minSleep);
    return {data: this._categories.all()};
  }


  public async create(element: ICategory): Promise<IResponse<ICategory>> {
    const elementValidation = this.validateForCreation(element);
    if (elementValidation) {
      return GlobalHelper.createErrorResponse<ICategory>(elementValidation);
    }

    await GlobalHelper.sleep(this._minSleep);

    element.id = element.id <= 0 ? this.getEmptyId() : element.id;
    
    if (this.existsById(element.id)) {
      return GlobalHelper.createErrorResponse<ICategory>('MsgIdAlreadyExist');
    }
    if (this.existsByName(element.name)) {
      return GlobalHelper.createErrorResponse<ICategory>('MsgNameAlreadyExist');
    }

    this._categories.push(element);

    return {data: element};
  }

  public async remove(elementId: number): Promise<IResponse<boolean>> {
    await GlobalHelper.sleep(this._minSleep);
    const removedEntries = this._categories.remove(p => p.id === elementId);
    return {data: removedEntries === 1};
  }


  public validateForCreation(element: ICategory): string {
    if (!element) {
      return 'MsgUndefined';
    }
    if (!element.name) {
      return 'MsgNameMissed';
    }
    return undefined;
  }

  public validateForUpdate(element: ICategory): string {
    if (!element) {
      return 'MsgUndefined';
    }
    if (element.id <= 0) {
      return 'MsgIdMissed';
    }
    if (!element.name) {
      return 'MsgNameMissed';
    }
    return undefined;
  }

  
  // Fake-Backend Helper-Functions:

  private existsById(id: number): boolean {
    return this._categories.exists(p => p.id === id);
  }

  private existsByName(name: string): boolean {
    return this._categories.exists(p => p.name === name);
  }

  private getEmptyId(): number {
    for(let index = 1; index < 10000000; index++) {
      if (!this._categories.exists(p => p.id === index)) {
        return index;
      }
    }
    throwError('No free ID found!');
  }
}
