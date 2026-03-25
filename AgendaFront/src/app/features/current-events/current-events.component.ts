import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-current-events',
  templateUrl: './current-events.component.html',
  styleUrls: ['./current-events.component.scss']
})
export class CurrentEventsComponent {

  @Input() currentEvent: any;
  @Output() onOpenEditModal = new EventEmitter<any>();
  @Output() onConfirmDelete = new EventEmitter<number>();
}
