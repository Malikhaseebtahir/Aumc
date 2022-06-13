import { TestBed } from '@angular/core/testing';

import { GroupNewsLetterService } from './group-news-letter.service';

describe('GroupNewsLetterService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: GroupNewsLetterService = TestBed.get(GroupNewsLetterService);
    expect(service).toBeTruthy();
  });
});
