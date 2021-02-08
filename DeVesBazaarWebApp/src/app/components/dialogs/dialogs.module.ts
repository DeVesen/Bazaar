import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { BasicsModule } from '../basics/basics.module';
import { PrimengModule } from '../primeng/primeng.module';

import { CategoryCreateDialogComponent } from './category-create-dialog/category-create-dialog.component';
import { ManufacturerCreateDialogComponent } from './manufacturer-create-dialog/manufacturer-create-dialog.component';
import { SellerCreateDialogComponent } from './seller-create-dialog/seller-create-dialog.component';



@NgModule({
  declarations: [
    ManufacturerCreateDialogComponent,
    CategoryCreateDialogComponent,
    SellerCreateDialogComponent
  ],
  imports: [
    CommonModule,
    FormsModule,

    BasicsModule,
    PrimengModule
  ],
  exports: [
    ManufacturerCreateDialogComponent,
    CategoryCreateDialogComponent,
    SellerCreateDialogComponent
  ]
})
export class DialogsModule { }
