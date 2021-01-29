import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import {ButtonModule} from 'primeng/button';

import {MenuModule} from 'primeng/menu';
import {ChartModule} from 'primeng/chart';
import {ConfirmDialogModule} from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api';

import {TableModule} from 'primeng/table';
import {OrderListModule} from 'primeng/orderlist';


@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  exports: [
    ButtonModule,
    
    MenuModule,
    ChartModule,
    ConfirmDialogModule,
    
    TableModule,
    OrderListModule
  ],
  providers: [ConfirmationService],
})
export class PrimengModule { }
