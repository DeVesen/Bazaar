import { TestBed } from '@angular/core/testing';

import { ActivePageInfoService } from './active-page-info.service';

describe('ActivePageInfoService', () => {
  let service: ActivePageInfoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ActivePageInfoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
