import { Component, OnInit } from '@angular/core';
import { ICategory } from 'src/app/models/category-model';

@Component({
  selector: 'app-category-administration-page',
  templateUrl: './category-administration-page.component.html',
  styleUrls: ['./category-administration-page.component.scss']
})
export class CategoryAdministrationPageComponent implements OnInit {

  categoriesLoaded: boolean;
  categories: ICategory;


  constructor() { }


  ngOnInit(): void {
  }


  onAddNewCategory(): void {

  }

  onReLoadCategory(): void {

  }

  onRemoveManufacturer(category: ICategory): void {
    
  }
  
}
