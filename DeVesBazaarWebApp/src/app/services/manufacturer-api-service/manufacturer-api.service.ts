import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { IManufacturer } from 'src/app/models/manufacturer-model';

@Injectable({
  providedIn: 'root'
})
export class ManufacturerApiService {

  private _manufacturer: IManufacturer[] = [];

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
      
      'Helm',
      'Ski',
      'Brille',
      'Hose',
      'Schuhe',
      'Ski-Schuhe',
      'Snowboard-Schuhe',
      'Stöcke',
      
      'Helm',
      'Ski',
      'Brille',
      'Hose',
      'Schuhe',
      'Ski-Schuhe',
      'Snowboard-Schuhe',
      'Stöcke',

      'Sven R.'
    ].forEach((v, i) => this._manufacturer.push({id: i, name: v}));
  }


  public async getAllManufacturerAsync(): Promise<IManufacturer[]> {
    return of(this._manufacturer).toPromise();
  }

  public async removeManufacturer(manufacturerId: number): Promise<void> {
    console.log('removeManufacturer', manufacturerId);
    return of(null).toPromise();
  }
}
