import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Profile } from '../models/profile';
import { PortfolioItem } from '../models/portfolio-item';
import { environment } from '../../environments/environment';
import { DataWithPagination } from '../models/data-with-pagination';

@Injectable({
  providedIn: 'root',
})
export class ProfileService {
  private apiUrl = `${environment.apiUrl}/profiles`;

  constructor(private http: HttpClient) {}

  getProfile(profileId: number): Observable<Profile> {
    return this.http.get<Profile>(`${this.apiUrl}/${profileId}`);
  }

  getPortfolioItems(profileId: number, pageNumber: number, pageSize: number): Observable<DataWithPagination<PortfolioItem>> {
    return this.http.get<DataWithPagination<PortfolioItem>>(`${this.apiUrl}/${profileId}/portfolio-items?pageNumber=${pageNumber}&pageSize=${pageSize}`);
  }

  createProfile(profile: Profile): Observable<Profile> {
    return this.http.post<Profile>(this.apiUrl, profile);
  }

  createPortfolioItem(profileId: number, item: PortfolioItem): Observable<PortfolioItem> {
    return this.http.post<PortfolioItem>(`${this.apiUrl}/${profileId}/portfolio-items`, item);
  }

  updateProfile(profile: Profile): Observable<Profile> {
    return this.http.put<Profile>(`${this.apiUrl}/${profile.id}`, profile);
  }

  updatePortfolioItem(item: PortfolioItem): Observable<PortfolioItem> {
    return this.http.put<PortfolioItem>(`${this.apiUrl}/portfolio-items/${item.id}`, item);
  }

  deleteProfile(profileId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${profileId}`);
  }

  deletePortfolioItem(itemId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/portfolio-items/${itemId}`);
  }
}
