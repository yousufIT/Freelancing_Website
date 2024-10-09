import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Review } from '../models/review';
import { environment } from '../../environments/environment';
import { DataWithPagination } from '../models/data-with-pagination';
import { ReviewForCreate } from '../models/for-create/review-for-create';

@Injectable({
  providedIn: 'root',
})
export class ReviewService {
  private apiUrl = `${environment.apiUrl}/Reviews`;

  constructor(private http: HttpClient) {}

  getReviewsByFreelancerId(freelancerId: number, pageNumber: number, pageSize: number): Observable<DataWithPagination<Review>> {
    return this.http.get<DataWithPagination<Review>>(`${this.apiUrl}/freelancer/${freelancerId}?pageNumber=${pageNumber}&pageSize=${pageSize}`);
  }

  getReviewsByClientId(clientId: number, pageNumber: number, pageSize: number): Observable<DataWithPagination<Review>> {
    return this.http.get<DataWithPagination<Review>>(`${this.apiUrl}/client/${clientId}?pageNumber=${pageNumber}&pageSize=${pageSize}`);
  }

  createReview(clientId:number,freelancerId:number,review: ReviewForCreate): Observable<Review> {
    return this.http.post<Review>(`${this.apiUrl}/Client/${clientId}/Freelancer/${freelancerId}`, review);
  }

  updateReview(id:number,review: ReviewForCreate): Observable<Review> {
    return this.http.put<Review>(`${this.apiUrl}/${id}`, review);
  }

  deleteReview(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
