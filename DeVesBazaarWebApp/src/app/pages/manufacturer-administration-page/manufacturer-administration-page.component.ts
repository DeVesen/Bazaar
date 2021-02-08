import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MediaObserver } from '@angular/flex-layout';
import { ConfirmationService, MenuItem } from 'primeng/api';
import { Subscription } from 'rxjs';
import { ManufacturerCreateDialogComponent } from 'src/app/components/manufacturer-create-dialog/manufacturer-create-dialog.component';
import { IManufacturer } from 'src/app/models/manufacturer-model';
import { ActivePageInfoService } from 'src/app/services/active-page-info-service/active-page-info.service';
import { StringHelper } from 'src/app/helpers/string-helper';
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
  searchValue: string;
  

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

  newEntryDlgClosed(): void {
    this.searchValue = '';
    this.onReLoadManufacturer();
  }

  onSearchToggle(isOpen: boolean): void {
    const fullSizeSearch = this._mediaObserver.isActive('xs')
    this.searchIsActive = isOpen;
    this.searchAsMax = isOpen && fullSizeSearch;

    if (!isOpen) {
      this.searchValue = '';
      this.onReLoadManufacturer();
    }
  }

  onSearchChange($event: any): void {
    if ($event && $event.key === 'Enter') {
      this.onReLoadManufacturer(this.searchValue);
    }
  }

  public doSearch(filterValue?: string): void {
    this.onReLoadManufacturer(filterValue);
  }
  
  public async onReLoadManufacturer(filterValue?: string): Promise<void> {
    this.manufacturerLoaded = false;
    this.manufacturer = (await this._manufacturerApi.getAll()).data.filter(i => {
      
      if (!i || !filterValue || filterValue.trim().length <= 0) {
        return true;
      }

      if (StringHelper.containsIgnorCase(i.id.toString(), filterValue)) {
        return true;
      }
      if (StringHelper.containsIgnorCase(i.name, filterValue)) {
        return true;
      }

      return false;
    });
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
