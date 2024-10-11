import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ReviewForCreate } from 'src/app/models/for-create/review-for-create';
import { ReviewService } from 'src/app/services/review.service';

@Component({
  selector: 'app-review-create',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './review-create.component.html',
  styleUrl: './review-create.component.css'
})
export class ReviewCreateComponent implements OnInit {
  clientId!: number;
  freelancerId!: number;
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
    this.clientId = +this.route.snapshot.paramMap.get('clientId')!;
    this.freelancerId = +this.route.snapshot.paramMap.get('freelancerId')!;
  }

  submitReview(): void {
    this.reviewService.createReview(this.clientId, this.freelancerId, this.review).subscribe({
      next: (data) => {
        console.log('Review created successfully', data);
        this.router.navigate(['/review/freelancer/10']); 
      },
      error: (error) => {
        console.error('Error creating review:', error);
      }
    });
  }
}
