import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MediaObserver } from '@angular/flex-layout';
import { ConfirmationService, MenuItem } from 'primeng/api';
import { Subscription } from 'rxjs';
import { StringHelper } from 'src/app/helpers/string-helper';
import { IManufacturer } from 'src/app/models/manufacturer-model';
import { ActivePageInfoService } from 'src/app/services/active-page-info-service/active-page-info.service';
import { SellerCreateDialogComponent } from '../../components/dialogs/seller-create-dialog/seller-create-dialog.component';
import { SellerApiService } from '../../services/seller-api-service/seller-api.service';

@Component({
  selector: 'app-seller-administration-page',
  templateUrl: './seller-administration-page.component.html',
  styleUrls: ['./seller-administration-page.component.scss']
})
export class SellerAdministrationPageComponent implements OnInit, OnDestroy {
  private mediaSub: Subscription;

  @ViewChild(SellerCreateDialogComponent) _createNewDialog: SellerCreateDialogComponent;

  sellerLoaded: boolean;
  sellers: IManufacturer[];
  actionItems: MenuItem[];
  searchIsActive = false;
  searchAsMax = false;
  searchValue: string;
  

  constructor(private _mediaObserver: MediaObserver,
              private _sellerApi: SellerApiService,
              private _confirmationService: ConfirmationService,
              private activePageInfo: ActivePageInfoService) { }


  get searchStyle(): string {
    return this.searchAsMax ? 'width: 100%' : '';
  }


  ngOnInit() {
    this.activePageInfo.setPageTitel('Verkäufer');
    this.onReLoadSeller();

    this.mediaSub = this._mediaObserver.asObservable().subscribe(x => {
      this.onSearchToggle(this.searchIsActive);
    });
  }

  ngOnDestroy() {
    this.mediaSub.unsubscribe();
  }


  public onAddNewSeller(): void {
    this._createNewDialog.showDialog();
  }

  newEntryDlgClosed(): void {
    this.searchValue = '';
    this.onReLoadSeller();
  }

  onSearchToggle(isOpen: boolean): void {
    const fullSizeSearch = this._mediaObserver.isActive('xs')
    this.searchIsActive = isOpen;
    this.searchAsMax = isOpen && fullSizeSearch;

    if (!isOpen) {
      this.searchValue = '';
      this.onReLoadSeller();
    }
  }

  onSearchChange($event: any): void {
    if ($event && $event.key === 'Enter') {
      this.onReLoadSeller(this.searchValue);
    }
  }

  public doSearch(filterValue?: string): void {
    this.onReLoadSeller(filterValue);
  }
  
  public async onReLoadSeller(filterValue?: string): Promise<void> {
    this.sellerLoaded = false;
    this.sellers = (await this._sellerApi.getAll()).data.filter(i => {
      
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
    this.sellerLoaded = true;
  }

  public onRemoveSeller(product: IManufacturer): void {
    this._confirmationService.confirm({
        message: `Wirklich '${product.name}' löschen?`,
        accept: async () => {
            await this._sellerApi.remove(product.id);
            await this.onReLoadSeller();
        }
    });
  }
}
