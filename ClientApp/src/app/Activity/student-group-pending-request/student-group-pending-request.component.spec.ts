import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentGroupPendingRequestComponent } from './student-group-pending-request.component';

describe('StudentGroupPendingRequestComponent', () => {
  let component: StudentGroupPendingRequestComponent;
  let fixture: ComponentFixture<StudentGroupPendingRequestComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StudentGroupPendingRequestComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StudentGroupPendingRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
