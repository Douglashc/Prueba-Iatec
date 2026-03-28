import { Component, EventEmitter, Input, Output, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { EventService } from 'src/app/core/services/event.service';

@Component({
  selector: 'app-event-form',
  templateUrl: './event-form.component.html',
  styleUrls: ['./event-form.component.scss']
})
export class EventFormComponent 
{
  private fb = inject(FormBuilder);
  private eventService = inject(EventService);

  @Input() isVisible: boolean = false;
  @Output() onClose = new EventEmitter<void>();
  @Output() onSaved = new EventEmitter<void>();

  eventForm: FormGroup;
  errorMessage: string = '';
  isEditMode: boolean = false;

  private _selectedEvent: any = null;

  @Input() set selectedEventEdit(value: any) 
  {
    console.log(value);
    this._selectedEvent = value;
    if (value) {
      this.isEditMode = true;
      this.eventForm.patchValue({
        name: value.name,
        description: value.description,
        startDate: value.startDate ? value.startDate.substring(0, 16) : '',
        endDate: value.endDate ? value.endDate.substring(0, 16) : '',
        location: value.location,
        type: value.type,
        status: value.status
      });
    } else {
      this.isEditMode = false;
      this.eventForm.reset({ type: 0 });
    }
  }

  constructor() 
  {
    this.eventForm = this.fb.group({
      name: ['', [Validators.required]],
      description: [''],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      location: ['', Validators.required],
      type: [0, Validators.required],
      status: [true, Validators.required],
    });
  }

  close() 
  {
    this.isVisible = false;
    this.onClose.emit();
    this.eventForm.reset({ type: 0 });
    this.errorMessage = '';
    this._selectedEvent = null;
    this.isEditMode = false;
  }

  save() 
  {
    if (this.eventForm.valid) {
      const { invitedUsername, ...eventData } = this.eventForm.value;
      const payload = { ...eventData, type: parseInt(eventData.type, 10) };

      if (this.isEditMode) {
        this.eventService.updateEvent(this._selectedEvent.id, payload).subscribe({
          next: () => {
            this.onSaved.emit();
            this.close();
          },
          error: (err) => this.errorMessage = err.error?.message || 'Error al actualizar.'
        });
      } else {
        this.eventService.createEvent(payload).subscribe({
          next: (newEvent) => {
            this.onSaved.emit();
            this.close();
          },
          error: (err) => this.errorMessage = err.error?.message || 'Error al guardar.'
        });
      }
    }
  }
}