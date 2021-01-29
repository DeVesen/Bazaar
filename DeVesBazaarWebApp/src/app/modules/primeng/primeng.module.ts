import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';


import {MenuModule} from 'primeng/menu';
import {ChartModule} from 'primeng/chart';


@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  exports: [
    MenuModule,
    ChartModule
  ]
})
export class PrimengModule { }
