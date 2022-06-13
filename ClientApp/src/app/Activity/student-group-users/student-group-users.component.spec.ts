import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentGroupUsersComponent } from './student-group-users.component';

describe('StudentGroupUsersComponent', () => {
  let component: StudentGroupUsersComponent;
  let fixture: ComponentFixture<StudentGroupUsersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StudentGroupUsersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StudentGroupUsersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
