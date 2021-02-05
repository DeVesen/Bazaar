import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MediaObserver } from '@angular/flex-layout';
import { ConfirmationService, MenuItem } from 'primeng/api';
import { Subscription } from 'rxjs';
import { ManufacturerCreateDialogComponent } from 'src/app/components/manufacturer-create-dialog/manufacturer-create-dialog.component';
import { IManufacturer } from 'src/app/models/manufacturer-model';
import { ActivePageInfoService } from 'src/app/services/active-page-info-service/active-page-info.service';
import { ManufacturerApiService } from 'src/app/services/manufacturer-api-service/manufacturer-api.service';

@Component({
  selector: 'app-manufacturer-administration-page',
  templateUrl: './manufacturer-administration-page.component.html',
  styleUrls: ['./manufacturer-administration-page.component.scss']
})
export class ManufacturerAdministrationPageComponent implements OnInit, OnDestroy {
  private mediaSub: Subscription;

  @ViewChild(ManufacturerCreateDialogComponent) _createNewDialog: ManufacturerCreateDialogComponent;

  manufacturerLoaded: boolean;
  manufacturer: IManufacturer[];
  actionItems: MenuItem[];
  searchIsActive = false;
  searchAsMax = false;

  constructor(private _mediaObserver: MediaObserver,
              private _manufacturerApi: ManufacturerApiService,
              private _confirmationService: ConfirmationService,
              private activePageInfo: ActivePageInfoService) { }


  get searchStyle(): string {
    return this.searchAsMax ? 'width: 100%' : '';
  }


  ngOnInit() {
    this.activePageInfo.setPageTitel('Hersteller');
    this.onReLoadManufacturer();

    this.mediaSub = this._mediaObserver.asObservable().subscribe(x => {
      this.onSearchToggle(this.searchIsActive);
    });
  }

  ngOnDestroy() {
    this.mediaSub.unsubscribe();
  }


  public onAddNewManufacturer(): void {
    this._createNewDialog.showDialog();
  }

  onSearchToggle(isOpen: boolean): void {
    const fullSizeSearch = this._mediaObserver.isActive('xs')
    this.searchIsActive = isOpen;
    this.searchAsMax = isOpen && fullSizeSearch;
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
