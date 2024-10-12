import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { User } from '../models/user';
import { environment } from '../../environments/environment';
import { PasswordData } from '../models/password-data';
import { ClientForCreate } from '../models/for-create/client-for-create';
import { FreelancerForCreate } from '../models/for-create/freelancer-for-create';


@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = `${environment.apiUrl}/Accounts`;

  constructor(private http: HttpClient) {}

  login(credentials: { email: string; password: string }): Observable<User> {
    return this.http.post<User>(`${this.apiUrl}/Login`, credentials).pipe(
      catchError(this.handleError)
    );
  }

  registerClient(user: ClientForCreate): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/Client`, user);
  }
  registerFreelancer(user: FreelancerForCreate): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/Freelancer`, user);
  }
  logout(): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/Logout`, {});
  }
  changePassword(passwordData : PasswordData): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/ChangePassword`, passwordData).pipe(
      catchError(this.handleError)
    );
  }
  isLoggedIn(): boolean {
    return true;
  }
  
  private handleError(error: any): Observable<never> {
    // Handle the error as appropriate for your application
    console.error('An error occurred:', error);
    return throwError(() => new Error('Something went wrong; please try again later.'));
  }
}
