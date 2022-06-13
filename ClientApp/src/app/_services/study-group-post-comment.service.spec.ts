import { TestBed } from '@angular/core/testing';

import { StudyGroupPostCommentService } from './study-group-post-comment.service';

describe('StudyGroupPostCommentService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: StudyGroupPostCommentService = TestBed.get(StudyGroupPostCommentService);
    expect(service).toBeTruthy();
  });
});
