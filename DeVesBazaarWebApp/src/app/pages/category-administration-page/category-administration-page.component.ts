import { Component, OnInit } from '@angular/core';
import { ConfirmationService } from 'primeng/api';
import { ICategory } from 'src/app/models/category-model';
import { CategoryApiService } from '../../services/category-api-service/category-api.service';

@Component({
  selector: 'app-category-administration-page',
  templateUrl: './category-administration-page.component.html',
  styleUrls: ['./category-administration-page.component.scss']
})
export class CategoryAdministrationPageComponent implements OnInit {

  categoriesLoaded: boolean;
  categories: ICategory[];


  constructor(private _categoryApi: CategoryApiService,
              private _confirmationService: ConfirmationService) { }


  ngOnInit(): void {
    this.onReLoadCategories();
  }


  onAddNewCategory(): void {

  }

  async onReLoadCategories(): Promise<void> {
    this.categoriesLoaded = false;
    this.categories = (await this._categoryApi.getAll()).data;
    this.categoriesLoaded = true;
  }

  onRemoveManufacturer(category: ICategory): void {
    
  }
  
}
