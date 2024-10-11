import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink, RouterOutlet } from '@angular/router';
import { ReviewService } from 'src/app/services/review.service';

@Component({
  selector: 'app-delete-review',
  standalone: true,
  imports: [FormsModule,CommonModule,RouterLink],
  templateUrl: './delete-review.component.html',
  styleUrl: './delete-review.component.css'
})
export class DeleteReviewComponent implements OnInit {
  reviewId!: number;

  constructor(
    private reviewService: ReviewService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.reviewId = +this.route.snapshot.paramMap.get('reviewId')!;
  }

  deleteReview(): void {
    if (confirm('Are you sure you want to delete this review?')) {
      this.reviewService.deleteReview(this.reviewId).subscribe({
        next: () => {
          console.log('Review deleted successfully');
          this.router.navigate(['/review/freelancer/10']);  
        },
        error: (error) => {
          console.error('Error deleting review:', error);
        }
      });
    }
  }
}
