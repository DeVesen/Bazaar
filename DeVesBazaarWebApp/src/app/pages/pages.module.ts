import { CommonModule } from "@angular/common";
import { NgModule, Optional, SkipSelf, ModuleWithProviders } from "@angular/core";
import { AngularMaterialModule } from "../components/angular-material/angular-material.module";
import { BasicsModule } from "../components/basics/basics.module";
import { SellerModule } from "../components/seller/seller.module";
import { AngularInfoPageComponent } from "./angular-info-page/angular-info-page.component";
import { ComponentDemoComponent } from "./component-demo/component-demo.component";
import { SellerManagementPageComponent } from "./seller-management-page/seller-management-page.component";
import { ComponentDemoModule } from "../components/component-demo/component-demo.module";
import { ArticleManagementPageComponent } from "./article-management-page/article-management-page.component";
import { ArticleModule } from "../components/article/article.module";


@NgModule({
  declarations: [
    AngularInfoPageComponent,
    ComponentDemoComponent,
    SellerManagementPageComponent,
    ArticleManagementPageComponent
  ],
  imports: [
    CommonModule,
    BasicsModule,
    ArticleModule,
    ComponentDemoModule,
    AngularMaterialModule,
    SellerModule
  ],
  exports: [
    AngularInfoPageComponent,
    ComponentDemoComponent,
    SellerManagementPageComponent,
    ArticleManagementPageComponent
  ]
})
export class PagesModule {
  constructor(@Optional() @SkipSelf() parentModule: PagesModule) {
    if (parentModule) {
      throw new Error(
        'PagesModule is already loaded. Import it in the AppModule only');
    }
  }

  static forRoot(): ModuleWithProviders<PagesModule> {
    return {
      ngModule: PagesModule,
      providers: []
    };
  }
}
