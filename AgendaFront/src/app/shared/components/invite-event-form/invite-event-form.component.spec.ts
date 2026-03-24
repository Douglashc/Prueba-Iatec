import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InviteEventFormComponent } from './invite-event-form.component';

describe('InviteEventFormComponent', () => {
  let component: InviteEventFormComponent;
  let fixture: ComponentFixture<InviteEventFormComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [InviteEventFormComponent]
    });
    fixture = TestBed.createComponent(InviteEventFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
