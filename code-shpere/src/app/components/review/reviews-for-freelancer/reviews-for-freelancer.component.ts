import { Component, OnInit } from '@angular/core';
import { ReviewService } from 'src/app/services/review.service'; 
import { Review } from 'src/app/models/review';
import { DataWithPagination, PaginationMetaData } from 'src/app/models/data-with-pagination';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { Client } from 'src/app/models/client';
import { ClientService } from 'src/app/services/client.service';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-reviews-for-freelancer',
  standalone: true,
  imports: [FormsModule,CommonModule, RouterLink],
  templateUrl: './reviews-for-freelancer.component.html',
  styleUrls: ['./reviews-for-freelancer.component.css','../../../../assets/css/local-design.css'],
  providers:[ClientService]
})
export class ReviewsForFreelancerComponent implements OnInit {
  freelancerId: number; 
  reviews: Review[] = [];
  client!:Client;
  paginationMetaData!:PaginationMetaData;
  currentPage: number = 1;
  pageSize: number = 10;
  totalPageCount: number = 0;
  auth:AuthService;
  constructor(private route: ActivatedRoute,
    private router:Router,
    private reviewService: ReviewService,
    private authService:AuthService,
    private clientService:ClientService) {
      this.freelancerId = +route.snapshot.paramMap.get('freelancerId')!;
      this.auth=authService
    }

  ngOnInit(): void {
    this.fetchReviews();
  }

  fetchReviews(): void {
    this.reviewService.getReviewsByFreelancerId(this.freelancerId, this.currentPage, this.pageSize).subscribe({
      next: (data: DataWithPagination<Review>) => {
        this.reviews = data.items;
        this.paginationMetaData=data.paginationMetaData;
        this.totalPageCount = this.paginationMetaData.totalPageCount; 
        
      },
      error: (error) => {
        console.error('Error fetching reviews:', error);
      }
    });
  }

  goToPage(page: number): void {
    this.currentPage = page;
    this.fetchReviews();
  }
editReview(reviewId:number):void{
  this.router.navigate(['/freelancer',this.freelancerId,'review','update',reviewId])
}
deleteReview(reviewId:number):void{
  this.router.navigate(['/freelancer',this.freelancerId,'review','delete-review',reviewId])
}
}
