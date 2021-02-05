import { Component, OnInit, ViewChild } from '@angular/core';
import { ConfirmationService, MenuItem } from 'primeng/api';
import { CategoryCreateDialogComponent } from 'src/app/components/category-create-dialog/category-create-dialog/category-create-dialog.component';
import { ICategory } from 'src/app/models/category-model';
import { CategoryApiService } from '../../services/category-api-service/category-api.service';
import { ActivePageInfoService } from 'src/app/services/active-page-info-service/active-page-info.service';

@Component({
  selector: 'app-category-administration-page',
  templateUrl: './category-administration-page.component.html',
  styleUrls: ['./category-administration-page.component.scss']
})
export class CategoryAdministrationPageComponent implements OnInit {

  @ViewChild(CategoryCreateDialogComponent) _createNewDialog: CategoryCreateDialogComponent;

  categoriesLoaded: boolean;
  categories: ICategory[];
  actionItems: MenuItem[];


  constructor(private _categoryApi: CategoryApiService,
              private _confirmationService: ConfirmationService,
              private activePageInfo: ActivePageInfoService) { }


  ngOnInit(): void {
    this.activePageInfo.setPageTitel('Kategorien');
    this.onReLoadCategories();
  }


  onAddNewCategory(): void {
    this._createNewDialog.showDialog();
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
