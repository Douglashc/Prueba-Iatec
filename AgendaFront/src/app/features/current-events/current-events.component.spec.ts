import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CurrentEventsComponent } from './current-events.component';

describe('CurrentEventsComponent', () => {
  let component: CurrentEventsComponent;
  let fixture: ComponentFixture<CurrentEventsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CurrentEventsComponent]
    });
    fixture = TestBed.createComponent(CurrentEventsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
