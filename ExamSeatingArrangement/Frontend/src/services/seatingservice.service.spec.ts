import { TestBed } from '@angular/core/testing';

import { SeatingService } from './seating.service';

describe('SeatingserviceService', () => {
  let service: SeatingService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SeatingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
