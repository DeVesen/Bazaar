import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import {ButtonModule} from 'primeng/button';
import {InputTextModule} from 'primeng/inputtext';

import {MenuModule} from 'primeng/menu';
import {ChartModule} from 'primeng/chart';
import {DividerModule} from 'primeng/divider';

import {DialogModule} from 'primeng/dialog';
import {ConfirmDialogModule} from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api';
import {ProgressSpinnerModule} from 'primeng/progressspinner';

import {TableModule} from 'primeng/table';
import {OrderListModule} from 'primeng/orderlist';


@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  exports: [
    ButtonModule,
    InputTextModule,
    
    MenuModule,
    ChartModule,
    DividerModule,

    DialogModule,
    ConfirmDialogModule,
    ProgressSpinnerModule,
    
    TableModule,
    OrderListModule
  ],
  providers: [ConfirmationService],
})
export class PrimengModule { }
