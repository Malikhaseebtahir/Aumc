import { TestBed } from '@angular/core/testing';

import { StudentGroupPostService } from './student-group-post.service';

describe('StudentGroupPostService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: StudentGroupPostService = TestBed.get(StudentGroupPostService);
    expect(service).toBeTruthy();
  });
});
