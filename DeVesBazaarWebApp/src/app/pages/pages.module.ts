import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { BasicsModule } from '../components/basics/basics.module';
import { PrimengModule } from '../components/primeng/primeng.module';
import { DialogsModule } from '../components/dialogs/dialogs.module';

import { CategoryAdministrationPageComponent } from './category-administration-page/category-administration-page.component';
import { ComponentDemoComponent } from './component-demo/component-demo.component';
import { ManufacturerAdministrationPageComponent } from './manufacturer-administration-page/manufacturer-administration-page.component';
import { SalePageComponent } from './sale-page/sale-page.component';
import { SellerAdministrationPageComponent } from './seller-administration-page/seller-administration-page.component';
import { StatisticPageComponent } from './statistic-page/statistic-page.component';



@NgModule({
  declarations: [
    ManufacturerAdministrationPageComponent,
    CategoryAdministrationPageComponent,
    SalePageComponent,
    SellerAdministrationPageComponent,
    StatisticPageComponent,
    ComponentDemoComponent
  ],
  imports: [
    CommonModule,
    FormsModule,

    BasicsModule,
    PrimengModule,
    DialogsModule
  ],
  exports: [
    ManufacturerAdministrationPageComponent,
    CategoryAdministrationPageComponent,
    SalePageComponent,
    SellerAdministrationPageComponent,
    StatisticPageComponent,
    ComponentDemoComponent
  ],
})
export class PagesModule { }
