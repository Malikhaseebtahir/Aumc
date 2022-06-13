import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UniversityNewsComponent } from './university-news.component';

describe('UniversityNewsComponent', () => {
  let component: UniversityNewsComponent;
  let fixture: ComponentFixture<UniversityNewsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UniversityNewsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UniversityNewsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
