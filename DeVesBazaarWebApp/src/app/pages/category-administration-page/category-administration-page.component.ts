import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ConfirmationService, MenuItem } from 'primeng/api';
import { CategoryCreateDialogComponent } from 'src/app/components/category-create-dialog/category-create-dialog/category-create-dialog.component';
import { ICategory } from 'src/app/models/category-model';
import { CategoryApiService } from '../../services/category-api-service/category-api.service';
import { ActivePageInfoService } from 'src/app/services/active-page-info-service/active-page-info.service';
import { MediaObserver } from '@angular/flex-layout';
import { Subscription } from 'rxjs';

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

  onSearchToggle(isOpen: boolean): void {
    const fullSizeSearch = this._mediaObserver.isActive('xs')
    this.searchIsActive = isOpen;
    this.searchAsMax = isOpen && fullSizeSearch;
  }

  public doSearch(): void {
    console.log('Manufacturer - doSearch');
  }

  async onReLoadCategories(): Promise<void> {
    this.categoriesLoaded = false;
    this.categories = (await this._categoryApi.getAll()).data;
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
