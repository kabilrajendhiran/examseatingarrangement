import { TestBed } from '@angular/core/testing';

import { AppToasterService } from './app-toaster.service';

describe('AppToasterService', () => {
  let service: AppToasterService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AppToasterService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
