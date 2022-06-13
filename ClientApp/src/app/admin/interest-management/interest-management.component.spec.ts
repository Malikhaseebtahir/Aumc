import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InterestManagementComponent } from './interest-management.component';

describe('InterestManagementComponent', () => {
  let component: InterestManagementComponent;
  let fixture: ComponentFixture<InterestManagementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InterestManagementComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InterestManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
