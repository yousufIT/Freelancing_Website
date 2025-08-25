
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, catchError, Observable, throwError } from 'rxjs';
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

  private _isLoggedIn$ = new BehaviorSubject<boolean>(!!localStorage.getItem('token'));
  public isLoggedIn$ = this._isLoggedIn$.asObservable();

  constructor(private http: HttpClient) {}

  // Note: login only performs HTTP; storing token is done in the caller (LoginComponent)
  login(loginData: LoginData): Observable<AuthenticationData> {
    return this.http.post<AuthenticationData>(`${this.apiUrl}/Login`, loginData).pipe(
      catchError(this.handleError)
    );
  }

  setLoggedInState(isLoggedIn: boolean) {
    this._isLoggedIn$.next(isLoggedIn);
    this.triggerLocalStorageChange();
  }

  registerClient(user: ClientForCreate): Observable<AuthenticationData> {
    return this.http.post<AuthenticationData>(`${this.apiUrl}/Client`, user);
  }

  registerFreelancer(user: FreelancerForCreate): Observable<AuthenticationData> {
    return this.http.post<AuthenticationData>(`${this.apiUrl}/Freelancer`, user);
  }

  registerAdmin(user: any) {
  return this.http.post<any>(`${this.apiUrl}/Admin`, user);
}


  // Clear client auth state immediately, then call API (ignore API errors for UI)
  logout(): Observable<void> {
    // clear client state immediately so UI updates without waiting for network
    this.clearAuth();

    return this.http.post<void>(`${this.apiUrl}/Logout`, {}).pipe(
      catchError(err => {
        // keep client cleared even if logout API fails
        console.warn('Logout API error (ignored):', err);
        return throwError(() => err);
      })
    );
  }

  // helper to return token string (used by interceptor)
  getToken(): string | null {
    return localStorage.getItem('token');
  }

  // clear local storage and notify subscribers
  clearAuth(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('role');
    localStorage.removeItem('User-Id');
    this._isLoggedIn$.next(false);
    this.triggerLocalStorageChange();
  }

  getUserRole(): string {
    const role = localStorage.getItem('role');
    return role || '';
  }

  getUserId(): number {
    const userId = localStorage.getItem('User-Id');
    return userId !== null ? +userId : 0;
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }

  private handleError(error: any): Observable<never> {
    console.error('An error occurred:', error);
    return throwError(() => new Error('Something went wrong; please try again later.'));
  }

  // Trigger custom event for same-tab listeners (keeps backward compatibility)
  private triggerLocalStorageChange(): void {
    const event = new Event('localStorageChanged');
    window.dispatchEvent(event);
  }
}
