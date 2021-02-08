import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from 'src/app/app-routing.module';


import {ButtonModule} from 'primeng/button';
import {InputTextModule} from 'primeng/inputtext';

import {MenuModule} from 'primeng/menu';
import {MenubarModule} from 'primeng/menubar';
import {ToolbarModule} from 'primeng/toolbar';

import {ChartModule} from 'primeng/chart';
import {DividerModule} from 'primeng/divider';

import {DialogModule} from 'primeng/dialog';
import {ConfirmDialogModule} from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api';
import {ProgressSpinnerModule} from 'primeng/progressspinner';

import {TableModule} from 'primeng/table';
import {OrderListModule} from 'primeng/orderlist';


import { ProgressSpinnerComponent } from './progress-spinner/progress-spinner.component';
import { SimpleInputComponent } from './simple-input/simple-input.component';


@NgModule({
  declarations: [
    ProgressSpinnerComponent,
    SimpleInputComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    
    ButtonModule,
    InputTextModule,
    
    MenuModule,
    MenubarModule,
    ToolbarModule,
    
    ChartModule,
    DividerModule,

    DialogModule,
    ConfirmDialogModule,
    ProgressSpinnerModule,
    
    TableModule,
    OrderListModule
  ],
  exports: [
    ButtonModule,
    InputTextModule,
    
    MenuModule,
    MenubarModule,
    ToolbarModule,
    
    ChartModule,
    DividerModule,

    DialogModule,
    ConfirmDialogModule,
    ProgressSpinnerModule,
    
    TableModule,
    OrderListModule,
    
    
    ProgressSpinnerComponent,
    SimpleInputComponent
  ],
  providers: [ConfirmationService],
})
export class PrimengModule { }
