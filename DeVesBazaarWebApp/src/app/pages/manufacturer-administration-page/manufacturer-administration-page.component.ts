import { Component, OnInit, ViewChild } from '@angular/core';
import { ConfirmationService, MenuItem } from 'primeng/api';
import { ManufacturerCreateDialogComponent } from 'src/app/components/manufacturer-create-dialog/manufacturer-create-dialog/manufacturer-create-dialog.component';
import { IManufacturer } from 'src/app/models/manufacturer-model';
import { ActivePageInfoService } from 'src/app/services/active-page-info-service/active-page-info.service';
import { ManufacturerApiService } from 'src/app/services/manufacturer-api-service/manufacturer-api.service';

@Component({
  selector: 'app-manufacturer-administration-page',
  templateUrl: './manufacturer-administration-page.component.html',
  styleUrls: ['./manufacturer-administration-page.component.scss']
})
export class ManufacturerAdministrationPageComponent implements OnInit {

  @ViewChild(ManufacturerCreateDialogComponent) _createNewDialog: ManufacturerCreateDialogComponent;

  manufacturerLoaded: boolean;
  manufacturer: IManufacturer[];
  actionItems: MenuItem[];

  constructor(private _manufacturerApi: ManufacturerApiService,
              private _confirmationService: ConfirmationService,
              private activePageInfo: ActivePageInfoService) { }

  ngOnInit() {
    this.activePageInfo.setPageTitel('Hersteller');
    this.onReLoadManufacturer();
  }


  public onAddNewManufacturer(): void {
    this._createNewDialog.showDialog();
  }

  public doSearch(): void {
    console.log('Manufacturer - doSearch');
  }
  
  public async onReLoadManufacturer(): Promise<void> {
    this.manufacturerLoaded = false;
    this.manufacturer = (await this._manufacturerApi.getAll()).data;
    this.manufacturerLoaded = true;
  }

  public onRemoveManufacturer(product: IManufacturer): void {
    this._confirmationService.confirm({
        message: `Wirklich '${product.name}' löschen?`,
        accept: async () => {
            await this._manufacturerApi.remove(product.id);
            await this.onReLoadManufacturer();
        }
    });
  }
}
