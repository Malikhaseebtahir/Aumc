import { TestBed } from '@angular/core/testing';

import { StudentGroupPostCommentService } from './student-group-post-comment.service';

describe('StudentGroupPostCommentService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: StudentGroupPostCommentService = TestBed.get(StudentGroupPostCommentService);
    expect(service).toBeTruthy();
  });
});
