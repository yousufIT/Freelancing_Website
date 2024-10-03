import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Review } from '../models/review';
import { environment } from '../../environments/environment';
import { DataWithPagination } from '../models/data-with-pagination';

@Injectable({
  providedIn: 'root',
})
export class ReviewService {
  private apiUrl = `${environment.apiUrl}/reviews`;

  constructor(private http: HttpClient) {}

  getReviewsByFreelancerId(freelancerId: number, pageNumber: number, pageSize: number): Observable<DataWithPagination<Review>> {
    return this.http.get<DataWithPagination<Review>>(`${this.apiUrl}/freelancer/${freelancerId}?pageNumber=${pageNumber}&pageSize=${pageSize}`);
  }

  getReviewsByClientId(clientId: number, pageNumber: number, pageSize: number): Observable<DataWithPagination<Review>> {
    return this.http.get<DataWithPagination<Review>>(`${this.apiUrl}/client/${clientId}?pageNumber=${pageNumber}&pageSize=${pageSize}`);
  }

  createReview(review: Review): Observable<Review> {
    return this.http.post<Review>(this.apiUrl, review);
  }

  updateReview(review: Review): Observable<Review> {
    return this.http.put<Review>(`${this.apiUrl}/${review.id}`, review);
  }

  deleteReview(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
