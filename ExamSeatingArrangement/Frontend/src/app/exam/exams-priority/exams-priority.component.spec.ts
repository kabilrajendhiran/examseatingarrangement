import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExamsPriorityComponent } from './exams-priority.component';

describe('ExamsPriorityComponent', () => {
  let component: ExamsPriorityComponent;
  let fixture: ComponentFixture<ExamsPriorityComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExamsPriorityComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExamsPriorityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
