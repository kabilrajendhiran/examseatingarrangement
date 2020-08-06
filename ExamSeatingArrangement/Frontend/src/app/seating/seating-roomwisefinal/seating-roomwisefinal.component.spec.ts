import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SeatingRoomwisefinalComponent } from './seating-roomwisefinal.component';

describe('SeatingRoomwisefinalComponent', () => {
  let component: SeatingRoomwisefinalComponent;
  let fixture: ComponentFixture<SeatingRoomwisefinalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SeatingRoomwisefinalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SeatingRoomwisefinalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
