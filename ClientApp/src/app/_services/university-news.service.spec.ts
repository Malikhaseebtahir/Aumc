import { TestBed } from '@angular/core/testing';

import { UniversityNewsService } from './university-news.service';

describe('UniversityNewsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: UniversityNewsService = TestBed.get(UniversityNewsService);
    expect(service).toBeTruthy();
  });
});
