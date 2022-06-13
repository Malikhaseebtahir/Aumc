import { TestBed } from '@angular/core/testing';

import { StudentGroupNewsLetterService } from './student-group-news-letter.service';

describe('StudentGroupNewsLetterService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: StudentGroupNewsLetterService = TestBed.get(StudentGroupNewsLetterService);
    expect(service).toBeTruthy();
  });
});
