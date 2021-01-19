import { TestBed } from '@angular/core/testing';
import { AngularMaterialModule } from 'src/app/components/angular-material/angular-material.module';

import { SellerUiActionsService } from './seller-ui-actions.service';

describe('SellerUiActionsService', () => {
  let service: SellerUiActionsService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        AngularMaterialModule
      ]
    });
    service = TestBed.inject(SellerUiActionsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
