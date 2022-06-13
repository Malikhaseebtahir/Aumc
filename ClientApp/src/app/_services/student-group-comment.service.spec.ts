import { TestBed } from '@angular/core/testing';

import { StudentGroupCommentService } from './student-group-comment.service';

describe('StudentGroupCommentService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: StudentGroupCommentService = TestBed.get(StudentGroupCommentService);
    expect(service).toBeTruthy();
  });
});
