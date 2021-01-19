import { ModuleWithProviders, NgModule, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DateTimePipe } from './date-time/date-time.pipe';



@NgModule({
  declarations: [
    DateTimePipe
  ],
  imports: [
    CommonModule
  ],
  exports: [
    DateTimePipe
  ]
})
export class PipesModule {
  constructor(@Optional() @SkipSelf() parentModule: PipesModule) {
    if (parentModule) {
      throw new Error(
        'PipesModule is already loaded. Import it in the AppModule only');
    }
  }

  static forRoot(): ModuleWithProviders<PipesModule> {
    return {
      ngModule: PipesModule,
      providers: []
    };
  }
}

