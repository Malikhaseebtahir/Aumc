import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PendingGroupsManagementComponent } from './pending-groups-management.component';

describe('PendingGroupsManagementComponent', () => {
  let component: PendingGroupsManagementComponent;
  let fixture: ComponentFixture<PendingGroupsManagementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PendingGroupsManagementComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PendingGroupsManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
