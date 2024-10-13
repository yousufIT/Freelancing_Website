import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PortfolioItem } from '../models/portfolio-item';
import { PortfolioItemForCreate } from '../models/for-create/portfolio-item-for-create';
import { DataWithPagination } from '../models/data-with-pagination';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {
  private apiUrl = 'https://localhost:7240/api/Profiles';

  constructor(private http: HttpClient) {}

  getPortfolioItems(ProfileId : number, pageNumber: number, pageSize: number): Observable<DataWithPagination<PortfolioItem>> {
    return this.http.get<DataWithPagination<PortfolioItem>>(`${this.apiUrl}/${ProfileId}/portfolio?pageNumber=${pageNumber}&pageSize=${pageSize}`);
  }

  createPortfolioItem(ProfileId : number ,item: PortfolioItemForCreate): Observable<PortfolioItem> {
    return this.http.post<PortfolioItem>(`${this.apiUrl}/${ProfileId}/portfolio`, item);
  }

  updatePortfolioItem(PortfolioItemId : number ,item: PortfolioItemForCreate): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/portfolio/${PortfolioItemId}`, item);
  }

  deletePortfolioItem(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/portfolio/${id}`);
  }

  getPortfolioItemById(id: number): Observable<PortfolioItem> {
    return this.http.get<PortfolioItem>(`${this.apiUrl}/${id}`);
  }
}
