import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Freelancer } from '../models/freelancer';
import { Review } from '../models/review';
import { DataWithPagination } from '../models/data-with-pagination';
import { environment } from '../../environments/environment'; // Importing the environment

@Injectable({
  providedIn: 'root'
})
export class FreelancerService {

  private apiUrl = `${environment.apiUrl}/freelancers`; // Using environment apiUrl

  constructor(private http: HttpClient) { }
  getAllFreelancers(): Observable<Freelancer[]> {
    return this.http.get<Freelancer[]>(`${this.apiUrl}/freelancers`);
  }
  
  getFreelancerById(id: number): Observable<Freelancer> {
    return this.http.get<Freelancer>(`${this.apiUrl}/${id}`);
  }

  createFreelancer(freelancer: Freelancer): Observable<Freelancer> {
    return this.http.post<Freelancer>(this.apiUrl, freelancer);
  }

  updateFreelancer(freelancer: Freelancer): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${freelancer.id}`, freelancer);
  }

  deleteFreelancer(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  getReviewsForFreelancer(freelancerId: number, pageNumber: number, pageSize: number): Observable<DataWithPagination<Review>> {
    return this.http.get<DataWithPagination<Review>>(`${this.apiUrl}/${freelancerId}/reviews?pageNumber=${pageNumber}&pageSize=${pageSize}`);
  }
}
