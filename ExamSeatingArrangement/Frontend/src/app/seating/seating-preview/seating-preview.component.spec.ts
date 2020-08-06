import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SeatingPreviewComponent } from './seating-preview.component';

describe('SeatingPreviewComponent', () => {
  let component: SeatingPreviewComponent;
  let fixture: ComponentFixture<SeatingPreviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SeatingPreviewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SeatingPreviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
