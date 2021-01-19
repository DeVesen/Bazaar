import { TestBed } from '@angular/core/testing';

import { ArtikleApiService } from './artikle-api.service';

describe('ArtikleApiService', () => {
  let service: ArtikleApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ArtikleApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
