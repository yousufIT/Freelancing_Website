import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { environment } from '../../environments/environment';
import { ClientForCreate } from '../models/for-create/client-for-create';
import { FreelancerForCreate } from '../models/for-create/freelancer-for-create';
import { AuthenticationData } from '../models/authentication-data';
import { LoginData } from '../models/login-data';


@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = `${environment.apiUrl}/Accounts`;

  constructor(private http: HttpClient) {}

  login(loginData:LoginData): Observable<AuthenticationData> {
    return this.http.post<AuthenticationData>(`${this.apiUrl}/Login`, loginData).pipe(
      catchError(this.handleError)
    );
  }

  registerClient(user: ClientForCreate): Observable<AuthenticationData> {
    return this.http.post<AuthenticationData>(`${this.apiUrl}/Client`, user);
  }
  registerFreelancer(user: FreelancerForCreate): Observable<AuthenticationData> {
    return this.http.post<AuthenticationData>(`${this.apiUrl}/Freelancer`, user);
  } 
  logout():Observable<void> {
      localStorage.removeItem('token');
      localStorage.removeItem('role');
      localStorage.removeItem('User-Id');
      return this.http.post<void>(`${this.apiUrl}/Logout`,{});
  }
  
  getUserRole(): string {
    const role = localStorage.getItem('role');
    return role || ''; 
  }

  getUserId(): number {
    const userId = localStorage.getItem('User-Id');
    return userId !== null ? +userId : 0;

  }

  // isLoggedIn(): boolean {
  //   const token = localStorage.getItem('authToken');
  //   return !!token;
  // }
  private handleError(error: any): Observable<never> {
    console.error('An error occurred:', error);
    return throwError(() => new Error('Something went wrong; please try again later.'));
  }
}
