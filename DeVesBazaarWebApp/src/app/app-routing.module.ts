import { NgModule } from "@angular/core";
import { Routes, RouterModule, PreloadAllModules } from "@angular/router";
import { AngularInfoPageComponent } from "./pages/angular-info-page/angular-info-page.component";
import { ArticleManagementPageComponent } from "./pages/article-management-page/article-management-page.component";
import { ComponentDemoComponent } from "./pages/component-demo/component-demo.component";
import { SellerManagementPageComponent } from "./pages/seller-management-page/seller-management-page.component";


const routes: Routes = [
  {
    path: 'angular-info',
    component: AngularInfoPageComponent
  },
  {
    path: 'seller-management',
    component: SellerManagementPageComponent
  },
  {
    path: 'article-management',
    component: ArticleManagementPageComponent
  },
  {
    path: 'component-demo',
    component: ComponentDemoComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules, relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
