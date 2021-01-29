import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { PrimengModule } from './modules/primeng/primeng.module';

import { HeaderMenuBtnComponent } from './components/header-menu-btn/header-menu-btn.component';
import { ManufacturerAdministrationPageComponent } from './pages/manufacturer-administration-page/manufacturer-administration-page.component';
import { SalePageComponent } from './pages/sale-page/sale-page.component';
import { CategoryAdministrationPageComponent } from './pages/category-administration-page/category-administration-page.component';
import { SellerAdministrationPageComponent } from './pages/seller-administration-page/seller-administration-page.component';
import { StatisticPageComponent } from './pages/statistic-page/statistic-page.component';


@NgModule({
  declarations: [
    AppComponent,

    HeaderMenuBtnComponent,

    ManufacturerAdministrationPageComponent,
    CategoryAdministrationPageComponent,
    SalePageComponent,
    SellerAdministrationPageComponent,
    StatisticPageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    PrimengModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
