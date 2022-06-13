import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentGroupManagementComponent } from './student-group-management.component';

describe('StudentGroupManagementComponent', () => {
  let component: StudentGroupManagementComponent;
  let fixture: ComponentFixture<StudentGroupManagementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StudentGroupManagementComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StudentGroupManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
