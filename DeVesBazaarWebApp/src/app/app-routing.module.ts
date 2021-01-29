import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CategoryAdministrationPageComponent } from './pages/category-administration-page/category-administration-page.component';
import { ManufacturerAdministrationPageComponent } from './pages/manufacturer-administration-page/manufacturer-administration-page.component';
import { SalePageComponent } from './pages/sale-page/sale-page.component';
import { SellerAdministrationPageComponent } from './pages/seller-administration-page/seller-administration-page.component';
import { StatisticPageComponent } from './pages/statistic-page/statistic-page.component';

const routes: Routes = [
  {
    path: 'sale',
    component: SalePageComponent
  },
  {
    path: 'seller-administration',
    component: SellerAdministrationPageComponent
  },
  {
    path: 'manufacturer-administration',
    component: ManufacturerAdministrationPageComponent
  },
  {
    path: 'category-administration',
    component: CategoryAdministrationPageComponent
  },
  {
    path: 'statistics',
    component: StatisticPageComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
