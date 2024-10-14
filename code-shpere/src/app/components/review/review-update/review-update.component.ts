import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ReviewForCreate } from 'src/app/models/for-create/review-for-create';
import { ReviewService } from 'src/app/services/review.service';

@Component({
  selector: 'app-review-update',
  standalone: true,
  imports: [FormsModule,CommonModule,RouterLink],
  templateUrl: './review-update.component.html',
  styleUrls: ['./review-update.component.css','../../../../assets/css/local-design.css']
})
export class ReviewUpdateComponent  implements OnInit {
  reviewId!: number;
  review: ReviewForCreate = {
    rating: 0,
    comment: ''
  };
  freelancerId: number = 0;

  constructor(
    private reviewService: ReviewService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.reviewId = +this.route.snapshot.paramMap.get('reviewId')!;
    this.fetchReview();
  }

  fetchReview(): void {
    this.reviewService.getReviewById(this.reviewId).subscribe({
      next: (data) => {
        this.review = data;  // Load the fetched review data into the form
        this.freelancerId = data.freelancerId;
      },
      error: (error) => {
        console.error('Error fetching review:', error);
      }
    });
  }

  submitReview(): void {
    this.reviewService.updateReview(this.reviewId, this.review).subscribe({
      next: (data) => {
        console.log('Review updated successfully', data);
        this.router.navigate(['/review','freelancer',this.freelancerId]);  
      },
      error: (error) => {
        console.error('Error updating review:', error);
      }
    });
  }
  goToList():void{
    this.router.navigate(['/review','freelancer',this.freelancerId]);  
  }
}
