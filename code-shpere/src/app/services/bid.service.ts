import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Bid } from '../models/bid';
import { environment } from '../../environments/environment';
import { DataWithPagination } from '../models/data-with-pagination';
import { BidForCreate } from '../models/for-create/bid-for-create';

@Injectable({
  providedIn: 'root',
})
export class BidService {
  private apiUrl = `${environment.apiUrl}/Bids`;

  constructor(private http: HttpClient) {}

  getBidsByProjectId(projectId: number, pageNumber: number, pageSize: number): Observable<DataWithPagination<Bid>> {
    return this.http.get<DataWithPagination<Bid>>(`${this.apiUrl}/project/${projectId}?pageNumber=${pageNumber}&pageSize=${pageSize}`);
  }

  getBidsByFreelancerId(freelancerId: number, pageNumber: number, pageSize: number): Observable<DataWithPagination<Bid>> {
    return this.http.get<DataWithPagination<Bid>>(`${this.apiUrl}/freelancer/${freelancerId}?pageNumber=${pageNumber}&pageSize=${pageSize}`);
  }

  createBid(freelancerId : number,projectId : number,bid: BidForCreate): Observable<Bid> {
    return this.http.post<Bid>(`${this.apiUrl}/freelancer/${freelancerId}/project/${projectId}`, bid);
  }

  updateBid(id : number,bid: BidForCreate): Observable<Bid> {
    return this.http.put<Bid>(`${this.apiUrl}/${id}`, bid);
  }

  deleteBid(bidId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${bidId}`);
  }

  deleteBidsForProject(projectId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/project/${projectId}`);
  }
}
