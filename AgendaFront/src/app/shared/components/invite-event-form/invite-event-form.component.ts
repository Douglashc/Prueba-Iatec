import { Component, inject, Input, Output, EventEmitter } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { EventService } from 'src/app/core/services/event.service';

@Component({
  selector: 'app-invite-event-form',
  templateUrl: './invite-event-form.component.html',
  styleUrls: ['./invite-event-form.component.scss']
})
export class InviteEventFormComponent 
{
  @Input() isVisible: boolean = false;
  @Input() eventId!: number;
  @Output() onClose = new EventEmitter<void>();

  username: string = '';
  errorMessage: string = '';
  loading: boolean = false;

  private eventService = inject(EventService);
  private fb = inject(FormBuilder);

  constructor()
  {
    this.fb.group({
      username: ['', Validators.required]
    })
  }

  send() {
    this.loading = true;
    this.eventService.sendInvite(this.eventId, this.username).subscribe({
      next: () => {
        alert('¡Invitación enviada!');
        this.close();
      },
      error: (err) => {
        this.errorMessage = err.error || 'Usuario no encontrado';
        this.loading = false;
      }
    });
  }

  close() {
    this.username = '';
    this.errorMessage = '';
    this.isVisible = false;
    this.onClose.emit();
  }
}
