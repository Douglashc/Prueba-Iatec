import { Injectable, signal, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private readonly API_URL = environment.baseUrl;
  private http = inject(HttpClient);

  currentUser = signal<any>(null);

  login(credentials: any) {
    return this.http.post<any>(`${this.API_URL}/Auth/login`, credentials).pipe(
      tap(res => {
        localStorage.setItem('token', res.token);
        localStorage.setItem('user', res.username);
        this.currentUser.set(res);
      })
    );
  }

  register(userData: any): Observable<any> {
    return this.http.post(`${this.API_URL}/Auth/register`, userData);
  }

  logout() {
    localStorage.removeItem('token');
    this.currentUser.set(null);
  }
}
