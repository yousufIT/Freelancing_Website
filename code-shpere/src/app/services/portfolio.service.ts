import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PortfolioService {
  private apiUrl = 'https://localhost:7240/api/portfolioitems';

  constructor(private http: HttpClient) {}

  getPortfolioItems(): Observable<any> {
    return this.http.get<any>(this.apiUrl);
  }

  createPortfolioItem(item: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, item);
  }

  deletePortfolioItem(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${id}`);
  }
}
