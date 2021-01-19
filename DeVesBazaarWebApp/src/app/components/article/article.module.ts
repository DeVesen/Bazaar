import { ModuleWithProviders, NgModule, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PipesModule } from 'src/app/pipes/pipes.module';
import { AngularMaterialModule } from '../angular-material/angular-material.module';
import { BasicsModule } from '../basics/basics.module';
import { ArticleCardStatusIconsComponent } from './article-card/article-card-status-icons/article-card-status-icons.component';
import { ArticleCardSaleContentComponent } from './article-card/article-card-content/article-card-sale-content/article-card-sale-content.component';
import { ArticleCardInfoContentComponent } from './article-card/article-card-content/article-card-info-content/article-card-info-content.component';
import { ArticleCardBaseComponent } from './article-card/article-card-base/article-card-base.component';
import { ArticleCardSaleComponent } from './article-card/article-card-sale/article-card-sale.component';
import { ArticleCardInfoComponent } from './article-card/article-card-info/article-card-info.component';
import { ArticleListComponent } from './article-list/article-list.component';



@NgModule({
  declarations: [
    ArticleCardBaseComponent,
    ArticleCardInfoContentComponent,
    ArticleCardSaleContentComponent,
    ArticleCardStatusIconsComponent,
    ArticleCardSaleComponent,
    ArticleCardInfoComponent,
    ArticleListComponent
  ],
  imports: [
    CommonModule,
    AngularMaterialModule,
    BasicsModule,
    PipesModule
  ],
  exports: [
    ArticleCardSaleComponent,
    ArticleCardInfoComponent,
    ArticleListComponent
  ]
})
export class ArticleModule {
  constructor(@Optional() @SkipSelf() parentModule: ArticleModule) {
    if (parentModule) {
      throw new Error(
        'ArticleModule is already loaded. Import it in the AppModule only');
    }
  }

  static forRoot(): ModuleWithProviders<ArticleModule> {
    return {
      ngModule: ArticleModule,
      providers: []
    };
  }
}
