import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/user';
import { environment } from '../../environments/environment';
import { Client } from '../models/client';
import { Freelancer } from '../models/freelancer';
import { ClientForCreate } from '../models/for-create/client-for-create';
import { FreelancerForCreate } from '../models/for-create/freelancer-for-create';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = `${environment.apiUrl}/Accounts`;

  constructor(private http: HttpClient) {}

  login(credentials: { email: string; password: string }): Observable<User> {
    return this.http.post<User>(`${this.apiUrl}/Login`, credentials);
  }

  registerClient(user: ClientForCreate): Observable<Client> {
    return this.http.post<Client>(`${this.apiUrl}/Client`, user);
  }
  registerFreelancer(user: FreelancerForCreate): Observable<Freelancer> {
    return this.http.post<Freelancer>(`${this.apiUrl}/Freelancer`, user);
  }
  logout(): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/Logout`, {});
  }
  changePassword(credentials: { currentPassword: string; newPassword: string }): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/ChangePassword`, credentials);
  }
  isLoggedIn(): boolean {
    return true;
  }

}
