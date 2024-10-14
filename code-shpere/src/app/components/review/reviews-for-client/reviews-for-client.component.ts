import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { DataWithPagination, PaginationMetaData } from 'src/app/models/data-with-pagination';
import { Review } from 'src/app/models/review';
import { AuthService } from 'src/app/services/auth.service';
import { ReviewService } from 'src/app/services/review.service';

@Component({
  selector: 'app-reviews-for-client',
  standalone: true,
  imports: [FormsModule,CommonModule,RouterLink],
  templateUrl: './reviews-for-client.component.html',
  styleUrls: ['./reviews-for-client.component.css','../../../../assets/css/local-design.css']
})
export class ReviewsForClientComponent implements OnInit {
  clientId!:number;
  reviews: Review[] = [];
  paginationMetaData!:PaginationMetaData;
  currentPage: number = 1;
  pageSize: number = 10;
  totalPageCount: number = 0;
  
  
  constructor(private route: ActivatedRoute,
    public auth : AuthService,
    private router:Router,
    private reviewService: ReviewService) {
      let id = route.snapshot.paramMap.get('clientId');
      this.clientId=id?+id:0;
    }

  ngOnInit(): void {
    this.fetchReviews();
  }
  fetchReviews(): void {
    this.reviewService.getReviewsByClientId(this.clientId, this.currentPage, this.pageSize).subscribe({
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
  editReview(reviewId:number,freelancerId:number):void{
    this.router.navigate(['/freelancer',freelancerId,'review','update',reviewId])
  }
  deleteReview(reviewId:number,freelancerId:number):void{
    this.router.navigate(['/freelancer',freelancerId,'review','delete-review',reviewId])
  }
}
