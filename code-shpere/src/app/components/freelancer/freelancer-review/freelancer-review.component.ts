import { Component, OnInit } from '@angular/core';
import { FreelancerService } from '../../../services/freelancer.service'; 
import { Review } from '../../../models/review'; 
import { DataWithPagination } from '../../../models/data-with-pagination';

@Component({
  selector: 'app-freelancer-review',
  templateUrl: './freelancer-review.component.html',
  styleUrls: ['./freelancer-review.component.css']
})
export class FreelancerReviewComponent implements OnInit {
  reviews: Review[] = [];
  totalReviews: number = 0;
  pageNumber: number = 1; 
  pageSize: number = 10;

  constructor(private freelancerService: FreelancerService) {}

  ngOnInit() {
    this.loadReviews();
  }

  loadReviews() {
    const freelancerId = 1; 
    this.freelancerService.getReviewsForFreelancer(freelancerId, this.pageNumber, this.pageSize).subscribe(
      (data: DataWithPagination<Review>) => {
        this.reviews = data.data; 
        this.totalReviews = data.totalCount; 
      },
      error => {
        console.error('Error fetching reviews', error);
      }
    );
  }
}
