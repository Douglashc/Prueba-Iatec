import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
})
export class EventService {

    private readonly API_URL = environment.baseUrl;
    private http = inject(HttpClient);

    getDashboardSummary(date?: string, filter?: string): Observable<any> {
        let params = new HttpParams();
        if (date) params = params.set('date', date);
        if (filter) params = params.set('filter', filter);

        return this.http.get<any>(`${this.API_URL}/Events/dashboard`, { params });
    }

    createEvent(eventData: any): Observable<any> {
        return this.http.post<any>(`${this.API_URL}/Events`, eventData);
    }

    updateEvent(id: number, eventData: any): Observable<any> {
        return this.http.put(`${this.API_URL}/Events/${id}`, eventData);
    }

    changeEventStatus(id: number) : Observable<any>
    {
        return this.http.put<any>(`${this.API_URL}/Events/${id}/toggle_status`, {});
    }

    deleteEvent(id: number): Observable<any> {
        return this.http.delete(`${this.API_URL}/Events/${id}`);
    }

    getPendingInvitations(): Observable<any[]> {
        return this.http.get<any[]>(`${this.API_URL}/Events/invitations/pending`);
    }

    respondToInvitation(invitationId: number, accept: boolean): Observable<any> {
        return this.http.post(`${this.API_URL}/Events/invitations/${invitationId}/respond?accept=${accept}`, {});
    }

    sendInvite(eventId: number, username: string): Observable<any> {
        return this.http.post(`${this.API_URL}/Events/${eventId}/invite`, { username });
    }
}