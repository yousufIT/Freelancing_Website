import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Freelancer } from '../models/freelancer';
import { Review } from '../models/review';
import { DataWithPagination } from '../models/data-with-pagination';
import { environment } from '../../environments/environment'; // Importing the environment
import { FreelancerForCreate } from '../models/for-create/freelancer-for-create';

@Injectable({
  providedIn: 'root'
})
export class FreelancerService {

  private apiUrl = `${environment.apiUrl}/freelancers`; 

  constructor(private http: HttpClient) { }
  
  getFreelancerById(id: number): Observable<Freelancer> {
    return this.http.get<Freelancer>(`${this.apiUrl}/${id}`);
  }

  createFreelancer(freelancer: FreelancerForCreate): Observable<Freelancer> {
    return this.http.post<Freelancer>(this.apiUrl, freelancer);
  }

  updateFreelancer(id : number, freelancer: FreelancerForCreate): Observable<Freelancer> {
    return this.http.put<Freelancer>(`${this.apiUrl}/${id}`, freelancer);
  }

  deleteFreelancer(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
  
}
