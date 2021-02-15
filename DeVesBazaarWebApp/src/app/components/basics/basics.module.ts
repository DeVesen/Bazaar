import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from 'src/app/app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

// PRIMENG Modules
// https://www.primefaces.org/primeng/showcase/#/

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


// CUSTOMIZED Modules

import { ProgressSpinnerComponent } from './progress-spinner/progress-spinner.component';
import { HeaderMenuBtnComponent } from './header-menu-btn/header-menu-btn.component';
import { SimpleInputComponent } from './simple-input/simple-input.component';
import { SimpleToolbarBtnComponent } from './simple-toolbar/simple-toolbar-btn/simple-toolbar-btn.component';
import { SimpleToolbarAreaComponent } from './simple-toolbar/simple-toolbar-area/simple-toolbar-area.component';
import { SimpleToolbarNewBtnComponent } from './simple-toolbar/simple-toolbar-new-btn/simple-toolbar-new-btn.component';
import { SimpleToolbarRefreshBtnComponent } from './simple-toolbar/simple-toolbar-refresh-btn/simple-toolbar-refresh-btn.component';
import { SimpleToolbarSearchBtnComponent } from './simple-toolbar/simple-toolbar-search-btn/simple-toolbar-search-btn.component';
import { SimpleToolbarSearchInputComponent } from './simple-toolbar/simple-toolbar-search-input/simple-toolbar-search-input.component';
import { SimpleTableButtonComponent } from './simple-table-buttons/simple-table-button/simple-table-button.component';
import { SimpleButtonComponent } from './simple-buttons/simple-button/simple-button.component';
import { SimbleAddButtonComponent } from './simple-buttons/simble-add-button/simble-add-button.component';
import { SimbleRefreshButtonComponent } from './simple-buttons/simble-refresh-button/simble-refresh-button.component';
import { SimbleSearchButtonComponent } from './simple-buttons/simble-search-button/simble-search-button.component';
import { SimbleAngleRightButtonComponent } from './simple-buttons/simble-angle-right-button/simble-angle-right-button.component';




@NgModule({
  declarations: [
    // CUSTOMIZED Modules
    ProgressSpinnerComponent,
    SimpleInputComponent,
    HeaderMenuBtnComponent,
    SimpleToolbarAreaComponent,
    SimpleToolbarBtnComponent,
    SimpleToolbarNewBtnComponent,
    SimpleToolbarRefreshBtnComponent,
    SimpleToolbarSearchBtnComponent,
    SimpleToolbarSearchInputComponent,
    SimpleButtonComponent,
    SimbleAddButtonComponent,
    SimbleRefreshButtonComponent,
    SimbleSearchButtonComponent,
    SimbleAngleRightButtonComponent,
    SimpleTableButtonComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    
    // PRIMENG Modules
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
    // PRIMENG Modules
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
    
    // CUSTOMIZED Modules
    ProgressSpinnerComponent,
    SimpleInputComponent,
    HeaderMenuBtnComponent,
    SimpleToolbarAreaComponent,
    SimpleToolbarBtnComponent,
    SimpleToolbarNewBtnComponent,
    SimpleToolbarRefreshBtnComponent,
    SimpleToolbarSearchBtnComponent,
    SimpleToolbarSearchInputComponent,
    SimpleButtonComponent,
    SimbleAddButtonComponent,
    SimbleRefreshButtonComponent,
    SimbleSearchButtonComponent,
    SimbleAngleRightButtonComponent,
    SimpleTableButtonComponent
  ],
  providers: [ConfirmationService],
})
export class BasicsModule { }
