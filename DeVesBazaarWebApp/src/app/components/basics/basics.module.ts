import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HeaderMenuBtnComponent } from './header-menu-btn/header-menu-btn.component';

import { SimpleToolbarBtnComponent } from './simple-toolbar/basics/simple-toolbar-btn/simple-toolbar-btn.component';
import { SimpleToolbarAreaComponent } from './simple-toolbar/simple-toolbar-area/simple-toolbar-area.component';
import { SimpleToolbarNewBtnComponent } from './simple-toolbar/simple-toolbar-new-btn/simple-toolbar-new-btn.component';
import { SimpleToolbarRefreshBtnComponent } from './simple-toolbar/simple-toolbar-refresh-btn/simple-toolbar-refresh-btn.component';
import { SimpleToolbarSearchBtnComponent } from './simple-toolbar/simple-toolbar-search-btn/simple-toolbar-search-btn.component';
import { SimpleToolbarSearchInputComponent } from './simple-toolbar/simple-toolbar-search-input/simple-toolbar-search-input.component';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    HeaderMenuBtnComponent,
    
    SimpleToolbarAreaComponent,
    SimpleToolbarBtnComponent,
    SimpleToolbarNewBtnComponent,
    SimpleToolbarRefreshBtnComponent,
    SimpleToolbarSearchBtnComponent,
    SimpleToolbarSearchInputComponent
  ],
  imports: [
    CommonModule,
    FormsModule
  ],
  exports: [
    HeaderMenuBtnComponent,
    
    SimpleToolbarAreaComponent,
    SimpleToolbarBtnComponent,
    SimpleToolbarNewBtnComponent,
    SimpleToolbarRefreshBtnComponent,
    SimpleToolbarSearchBtnComponent,
    SimpleToolbarSearchInputComponent
  ]
})
export class BasicsModule { }
