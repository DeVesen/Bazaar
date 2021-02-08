import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { BasicsModule } from '../basics/basics.module';
import { PrimengModule } from '../primeng/primeng.module';

import { CategoryCreateDialogComponent } from './category-create-dialog/category-create-dialog.component';
import { ManufacturerCreateDialogComponent } from './manufacturer-create-dialog/manufacturer-create-dialog.component';



@NgModule({
  declarations: [
    ManufacturerCreateDialogComponent,
    CategoryCreateDialogComponent
  ],
  imports: [
    CommonModule,
    FormsModule,

    BasicsModule,
    PrimengModule
  ],
  exports: [
    ManufacturerCreateDialogComponent,
    CategoryCreateDialogComponent
  ]
})
export class DialogsModule { }
