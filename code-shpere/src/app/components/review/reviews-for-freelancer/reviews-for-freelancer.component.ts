import { Component, OnInit } from '@angular/core';
import { ReviewService } from 'src/app/services/review.service'; 
import { Review } from 'src/app/models/review';
import { DataWithPagination, PaginationMetaData } from 'src/app/models/data-with-pagination';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-reviews-for-freelancer',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './reviews-for-freelancer.component.html',
  styleUrls: ['./reviews-for-freelancer.component.css']
})
export class ReviewsForFreelancerComponent implements OnInit {
  freelancerId: number; 
  reviews: Review[] = [];
  paginationMetaData!:PaginationMetaData;
  currentPage: number = 1;
  pageSize: number = 10;
  totalPageCount: number = 0;
  
  constructor(private route: ActivatedRoute,
    private reviewService: ReviewService) {
      let id = route.snapshot.paramMap.get('freelancerId');
      this.freelancerId=id?+id:0;
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
        console.log(data);
        
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
}
