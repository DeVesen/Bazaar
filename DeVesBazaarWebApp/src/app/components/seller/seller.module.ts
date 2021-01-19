import { ModuleWithProviders, NgModule, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AngularMaterialModule } from '../angular-material/angular-material.module';
import { SellerTableComponent } from './seller-table/seller-table.component';
import { SellerManagemendDialogComponent } from './seller-managemend-dialog/seller-managemend-dialog.component';
import { BasicsModule } from '../basics/basics.module';
import { SellerTableDetailRowComponent } from './seller-table-detail-row/seller-table-detail-row.component';
import { ValidatorsModule } from 'src/app/validators/validators.module';
import { SellerSelectionInputComponent } from './seller-selection-input/seller-selection-input.component';


@NgModule({
  declarations: [
    SellerTableComponent,
    SellerTableDetailRowComponent,
    SellerManagemendDialogComponent,
    SellerSelectionInputComponent
  ],
  imports: [
    CommonModule,
    AngularMaterialModule,
    BasicsModule,
    ValidatorsModule
  ],
  exports: [
    SellerTableComponent,
    SellerTableDetailRowComponent,
    SellerManagemendDialogComponent,
    SellerSelectionInputComponent
  ],
})
export class SellerModule {
  constructor(@Optional() @SkipSelf() parentModule: SellerModule) {
    if (parentModule) {
      throw new Error(
        'SellerModule is already loaded. Import it in the AppModule only');
    }
  }

  static forRoot(): ModuleWithProviders<SellerModule> {
    return {
      ngModule: SellerModule,
      providers: []
    };
  }
}
