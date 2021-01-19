import { TestBed } from '@angular/core/testing';

import { StatisticsApi } from './statistics-api.service';

describe('SellerStatisticsApiService', () => {
  let service: StatisticsApi;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StatisticsApi);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
