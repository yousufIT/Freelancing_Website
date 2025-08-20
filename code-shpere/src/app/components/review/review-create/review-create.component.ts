import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ReviewForCreate } from 'src/app/models/for-create/review-for-create';
import { ReviewService } from 'src/app/services/review.service';

@Component({
  selector: 'app-review-create',
  standalone: true,
  imports: [FormsModule,CommonModule,RouterLink],
  templateUrl: './review-create.component.html',
  styleUrls: ['./review-create.component.css']
})
export class ReviewCreateComponent implements OnInit {
  clientId!: number;
  freelancerId!: number;
  review: ReviewForCreate = {
    rating: 0,
    comment: '',
    clientId: 0,
    freelancerId: 0
  };

  constructor(
    private reviewService: ReviewService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.clientId = +this.route.snapshot.paramMap.get('clientId')!;
    this.freelancerId = +this.route.snapshot.paramMap.get('freelancerId')!;
  }

  submitReview(): void {
    this.reviewService.createReview(this.clientId, this.freelancerId, this.review).subscribe({
      next: (data) => {
        this.router.navigate(['/review/freelancer',this.freelancerId]); 
      },
      error: (err) => {
        if (err.status === 400 && err.error.message === "You have already left a review for this freelancer.") {
          alert("Error: You have already left a review for this freelancer.");
          this.router.navigate(['/review/freelancer',this.freelancerId]); 
        } else {
          console.log("An error occurred. Please try again.");
        }
      }
    });
  }
}
