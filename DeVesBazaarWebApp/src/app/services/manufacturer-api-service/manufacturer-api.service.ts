import { Injectable } from '@angular/core';
import { of, throwError } from 'rxjs';
import { IResponse } from 'src/app/models/api-response-model';
import { IManufacturer } from 'src/app/models/manufacturer-model';
import { GlobalHelper } from '../helpers/global-helper';
import { List } from '../helpers/list';


@Injectable({
  providedIn: 'root'
})
export class ManufacturerApiService {

  private _minSleep = 500;
  private _manufacturer = new List<IManufacturer>();

  constructor() {
    [
      'Helm',
      'Ski',
      'Brille',
      'Hose',
      'Schuhe',
      'Ski-Schuhe',
      'Snowboard-Schuhe',
      'Stöcke',

      'Sven R.'
    ].forEach(v => this._manufacturer.push({id: this.getEmptyId(), name: v}));

  }


  public async getAll(): Promise<IResponse<IManufacturer[]>> {
    await GlobalHelper.sleep(this._minSleep);
    return {data: this._manufacturer.all()};
  }


  public async create(element: IManufacturer): Promise<IResponse<IManufacturer>> {
    const elementValidation = this.validateForCreation(element);
    if (elementValidation) {
      return GlobalHelper.createErrorResponse<IManufacturer>(elementValidation);
    }

    await GlobalHelper.sleep(this._minSleep);

    element.id = element.id <= 0 ? this.getEmptyId() : element.id;
    
    if (this.existsById(element.id)) {
      return GlobalHelper.createErrorResponse<IManufacturer>('MsgIdAlreadyExist');
    }
    if (this.existsByName(element.name)) {
      return GlobalHelper.createErrorResponse<IManufacturer>('MsgNameAlreadyExist');
    }

    this._manufacturer.push(element);

    return {data: element};
  }

  public async remove(elementId: number): Promise<IResponse<boolean>> {
    await GlobalHelper.sleep(this._minSleep);
    const removedEntries = this._manufacturer.remove(p => p.id === elementId);
    return {data: removedEntries === 1};
  }


  public validateForCreation(element: IManufacturer): string {
    if (!element) {
      return 'MsgUndefined';
    }
    if (!element.name) {
      return 'MsgNameMissed';
    }
    return undefined;
  }

  public validateForUpdate(element: IManufacturer): string {
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
    return this._manufacturer.exists(p => p.id === id);
  }

  private existsByName(name: string): boolean {
    return this._manufacturer.exists(p => p.name === name);
  }

  private getEmptyId(): number {
    for(let index = 1; index < 10000000; index++) {
      if (!this._manufacturer.exists(p => p.id === index)) {
        return index;
      }
    }
    throwError('No free ID found!');
  }
}
