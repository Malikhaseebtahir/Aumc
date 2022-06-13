import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserJoinGroupsComponent } from './user-join-groups.component';

describe('UserJoinGroupsComponent', () => {
  let component: UserJoinGroupsComponent;
  let fixture: ComponentFixture<UserJoinGroupsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserJoinGroupsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserJoinGroupsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
