import { TestBed } from '@angular/core/testing';

import { ManufacturerApiService } from './manufacturer-api.service';

describe('ManufacturerApiService', () => {
  let service: ManufacturerApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ManufacturerApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
