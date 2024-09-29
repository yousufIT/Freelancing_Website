import { Component, OnInit } from '@angular/core';
import { ReviewService } from '../services/review.service';
import { FormsModule } from '@angular/forms';


@Component({
  standalone: true,
  selector: 'app-review',
  templateUrl: './review.component.html',
  imports: [FormsModule]
})
export class ReviewComponent implements OnInit {
  reviews: any[] = [];
  newReview = {
    comment: '',
    rating: 5
  };

  constructor(private reviewService: ReviewService) {}

  ngOnInit(): void {
    this.reviewService.getReviews().subscribe((data) => {
      this.reviews = data;
    });
  }

  submitReview() {
    this.reviewService.createReview(this.newReview).subscribe(() => {
      this.ngOnInit(); // Reload reviews after submission
    });
  }
}
