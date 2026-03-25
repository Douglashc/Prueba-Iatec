import { Component, Input, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-old-events',
  templateUrl: './old-events.component.html',
  styleUrls: ['./old-events.component.scss']
})
export class OldEventsComponent {

  @Input() oldEvent: any; 
  @Output() onConfirmDelete = new EventEmitter<number>();
}
