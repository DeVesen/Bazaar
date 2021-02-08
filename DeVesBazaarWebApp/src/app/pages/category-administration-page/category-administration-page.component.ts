import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ConfirmationService, MenuItem } from 'primeng/api';
import { ICategory } from 'src/app/models/category-model';
import { CategoryApiService } from '../../services/category-api-service/category-api.service';
import { ActivePageInfoService } from 'src/app/services/active-page-info-service/active-page-info.service';
import { MediaObserver } from '@angular/flex-layout';
import { Subscription } from 'rxjs';
import { StringHelper } from 'src/app/helpers/string-helper';
import { CategoryCreateDialogComponent } from 'src/app/components/dialogs/category-create-dialog/category-create-dialog.component';

@Component({
  selector: 'app-category-administration-page',
  templateUrl: './category-administration-page.component.html',
  styleUrls: ['./category-administration-page.component.scss']
})
export class CategoryAdministrationPageComponent implements OnInit, OnDestroy {
  private mediaSub: Subscription;

  @ViewChild(CategoryCreateDialogComponent) _createNewDialog: CategoryCreateDialogComponent;

  categoriesLoaded: boolean;
  categories: ICategory[];
  actionItems: MenuItem[];
  searchIsActive = false;
  searchAsMax = false;
  searchValue: string;


  constructor(private _mediaObserver: MediaObserver,
              private _categoryApi: CategoryApiService,
              private _confirmationService: ConfirmationService,
              private _activePageInfo: ActivePageInfoService) { }


  get searchStyle(): string {
    return this.searchAsMax ? 'width: 100%' : '';
  }


  ngOnInit(): void {
    this._activePageInfo.setPageTitel('Kategorien');
    this.onReLoadCategories();

    this.mediaSub = this._mediaObserver.asObservable().subscribe(x => {
      this.onSearchToggle(this.searchIsActive);
    });
  }

  ngOnDestroy() {
    this.mediaSub.unsubscribe();
  }


  onAddNewCategory(): void {
    this._createNewDialog.showDialog();
  }

  newEntryDlgClosed(): void {
    this.searchValue = '';
    this.onReLoadCategories();
  }

  onSearchToggle(isOpen: boolean): void {
    const fullSizeSearch = this._mediaObserver.isActive('xs')
    this.searchIsActive = isOpen;
    this.searchAsMax = isOpen && fullSizeSearch;

    if (!isOpen) {
      this.onReLoadCategories();
    }
  }

  onSearchChange($event: any): void {
    if ($event && $event.key === 'Enter') {
      this.onReLoadCategories(this.searchValue);
    }
  }

  public doSearch(filterValue?: string): void {
    this.onReLoadCategories(filterValue);
  }

  async onReLoadCategories(filterValue?: string): Promise<void> {
    this.categoriesLoaded = false;
    this.categories = (await this._categoryApi.getAll()).data.filter(i => {
      
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
    this.categoriesLoaded = true;
  }

  onRemoveManufacturer(category: ICategory): void {
    this._confirmationService.confirm({
        message: `Wirklich '${category.name}' löschen?`,
        accept: async () => {
            await this._categoryApi.remove(category.id);
            await this.onReLoadCategories();
        }
    });
  }
  
}
