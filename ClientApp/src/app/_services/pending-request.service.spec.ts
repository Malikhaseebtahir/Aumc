import { TestBed } from '@angular/core/testing';

import { PendingRequestService } from './pending-request.service';

describe('PendingRequestService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: PendingRequestService = TestBed.get(PendingRequestService);
    expect(service).toBeTruthy();
  });
});
