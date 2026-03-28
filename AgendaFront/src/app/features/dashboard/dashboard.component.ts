import { Component, inject, OnInit } from '@angular/core';
import { EventService } from '../../core/services/event.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  
  private eventService = inject(EventService);
  private router = inject(Router);
  public user = localStorage.getItem('user');

  myEvents: any[] = [];
  invitations: any[] = [];
  loading: boolean = true;
  showModal: boolean = false;
  selectedEvent: any = null;
  selectedEventId: any = null;
  showInviteModal: boolean = false;

  currentEvents: any[] = [];
  upcomingEvents: any[] = [];
  oldEvents: any[] = [];

  filterText: string = '';
  filterDate: string = '';

  ngOnInit() {
    this.refreshData();
  }

  refreshData() {
    this.loading = true;

    this.eventService.getDashboardSummary(this.filterDate, this.filterText).subscribe({
      next: (data) => {
        this.currentEvents = data.currentEvents;
        this.upcomingEvents = data.upcomingEvents;
        this.oldEvents = data.oldEvents;
        this.loading = false;
      },
      error: () => this.loading = false
    });

    this.eventService.getPendingInvitations().subscribe(data => this.invitations = data);
  }

  openEditModal(event: any) {
    console.log('HOLA');
    console.log("EVENT: ", event)
    this.selectedEvent = event;
    this.showModal = true;
  }

  handleCloseModal() {
    this.showModal = false;
    this.selectedEvent = null; 
  }

  changeEventStatus(id: number)
  {
    if(!id) return;

    if(confirm('Cambiar el estado del evento?'))
    {
      this.eventService.changeEventStatus(id).subscribe(() => this.refreshData());
    }
  }

  confirmDelete(id: number) {
    if (confirm('¿Estás seguro de eliminar este evento?')) {
      this.eventService.deleteEvent(id).subscribe(() => this.refreshData());
    }
  }

  clearFilters() {
    this.filterText = '';
    this.filterDate = '';
    this.refreshData();
  }

  handleInvitation(id: number, accept: boolean) {
    this.eventService.respondToInvitation(id, accept).subscribe(() => {
      this.refreshData();
    });
  }

  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }
}
