import { ModuleWithProviders, NgModule, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ValidatorsModule } from 'src/app/validators/validators.module';
import { AngularMaterialModule } from '../angular-material/angular-material.module';
import { BasicsModule } from '../basics/basics.module';
import { InputFieldDemoComponent } from './input-field-demo/input-field-demo.component';
import { ArticleCardDemoComponent } from './article-card-demo/article-card-demo.component';
import { ArticleModule } from '../article/article.module';
import { SellerSelectionInputDemoComponent } from './seller-selection-input-demo/seller-selection-input-demo.component';
import { SellerModule } from '../seller/seller.module';



@NgModule({
  declarations: [
    InputFieldDemoComponent,
    ArticleCardDemoComponent,
    SellerSelectionInputDemoComponent
  ],
  imports: [
    CommonModule,
    AngularMaterialModule,
    BasicsModule,
    ValidatorsModule,
    SellerModule,
    ArticleModule
  ],
  exports: [
    InputFieldDemoComponent,
    ArticleCardDemoComponent,
    SellerSelectionInputDemoComponent
  ],
})
export class ComponentDemoModule {
  constructor(@Optional() @SkipSelf() parentModule: ComponentDemoModule) {
    if (parentModule) {
      throw new Error(
        'ComponentDemoModule is already loaded. Import it in the AppModule only');
    }
  }

  static forRoot(): ModuleWithProviders<ComponentDemoModule> {
    return {
      ngModule: ComponentDemoModule,
      providers: []
    };
  }
}

