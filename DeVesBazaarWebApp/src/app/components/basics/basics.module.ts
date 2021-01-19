import { ModuleWithProviders, NgModule, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AngularMaterialModule } from '../angular-material/angular-material.module';
import { InputFieldComponent } from './input-field/input-field.component';
import { MessageBoxComponent } from './message-box/message-box.component';
import { PageTitleComponent } from './page-title/page-title.component';
import { AccordionComponent } from './accordion/accordion.component';
import { ExpansionPanelComponent } from './expansion-panel/expansion-panel.component';
import { LabelValueComponent } from './label-value/label-value.component';
import { PipesModule } from 'src/app/pipes/pipes.module';



@NgModule({
  declarations: [
    InputFieldComponent,
    MessageBoxComponent,
    PageTitleComponent,
    AccordionComponent,
    ExpansionPanelComponent,
    
    LabelValueComponent
  ],
  imports: [
    CommonModule,
    AngularMaterialModule,
    PipesModule
  ],
  exports: [
    InputFieldComponent,

    MessageBoxComponent,
    PageTitleComponent,
    AccordionComponent,
    ExpansionPanelComponent,
    
    LabelValueComponent
  ]
})
export class BasicsModule {
  constructor(@Optional() @SkipSelf() parentModule: BasicsModule) {
    if (parentModule) {
      throw new Error(
        'BasicsModule is already loaded. Import it in the AppModule only');
    }
  }

  static forRoot(): ModuleWithProviders<BasicsModule> {
    return {
      ngModule: BasicsModule,
      providers: []
    };
  }
}
