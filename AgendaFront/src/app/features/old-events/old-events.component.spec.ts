import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OldEventsComponent } from './old-events.component';

describe('OldEventsComponent', () => {
  let component: OldEventsComponent;
  let fixture: ComponentFixture<OldEventsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [OldEventsComponent]
    });
    fixture = TestBed.createComponent(OldEventsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
