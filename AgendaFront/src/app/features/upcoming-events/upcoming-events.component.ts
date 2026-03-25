import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-upcoming-events',
  templateUrl: './upcoming-events.component.html',
  styleUrls: ['./upcoming-events.component.scss']
})
export class UpcomingEventsComponent {

  @Input() event: any;
  @Output() onEdit = new EventEmitter<any>();
  @Output() onDelete = new EventEmitter<number>();
  @Output() onInvite = new EventEmitter<number>();

  getAvatarColor(name: string): string {
    const colors = ['bg-indigo-500', 'bg-emerald-500', 'bg-amber-500', 'bg-rose-500', 'bg-sky-500'];
    const charCode = name.charCodeAt(0);
    return colors[charCode % colors.length];
  }
}
