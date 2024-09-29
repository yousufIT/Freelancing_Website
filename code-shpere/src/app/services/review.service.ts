import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {
  private apiUrl = 'https://localhost:7240/api/reviews';

  constructor(private http: HttpClient) {}

  getReviews(): Observable<any> {
    return this.http.get<any>(this.apiUrl);
  }

  createReview(review: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, review);
  }

  deleteReview(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${id}`);
  }
}
