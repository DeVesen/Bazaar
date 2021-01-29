import { Component, OnInit } from '@angular/core';
import { ConfirmationService } from 'primeng/api';
import { IManufacturer } from 'src/app/models/manufacturer-model';
import { ManufacturerApiService } from 'src/app/services/manufacturer-api-service/manufacturer-api.service';

@Component({
  selector: 'app-manufacturer-administration-page',
  templateUrl: './manufacturer-administration-page.component.html',
  styleUrls: ['./manufacturer-administration-page.component.scss']
})
export class ManufacturerAdministrationPageComponent implements OnInit {

  manufacturer: IManufacturer[];

  constructor(private _manufacturerApi: ManufacturerApiService,
              private _confirmationService: ConfirmationService) { }

  ngOnInit() {
    this._manufacturerApi.getAllManufacturerAsync()
                         .then(x => this.manufacturer = x);
  }


  public onRemoveManufacturer(product: IManufacturer): void {
    this._confirmationService.confirm({
        message: `Wirklich '${product.name}' löschen?`,
        accept: () => {
            this._manufacturerApi.removeManufacturer(product.id);
        }
    });
  }
}
