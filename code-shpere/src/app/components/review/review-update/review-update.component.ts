import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ReviewForCreate } from 'src/app/models/for-create/review-for-create';
import { ReviewService } from 'src/app/services/review.service';

@Component({
  selector: 'app-review-update',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './review-update.component.html',
  styleUrl: './review-update.component.css'
})
export class ReviewUpdateComponent  implements OnInit {
  reviewId!: number;
  review: ReviewForCreate = {
    rating: 0,
    comment: ''
  };

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
        this.router.navigate(['/review/freelancer/10']);  
      },
      error: (error) => {
        console.error('Error updating review:', error);
      }
    });
  }
}
