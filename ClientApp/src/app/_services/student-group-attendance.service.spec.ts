import { TestBed } from '@angular/core/testing';

import { StudentGroupAttendanceService } from './student-group-attendance.service';

describe('StudentGroupAttendanceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: StudentGroupAttendanceService = TestBed.get(StudentGroupAttendanceService);
    expect(service).toBeTruthy();
  });
});
