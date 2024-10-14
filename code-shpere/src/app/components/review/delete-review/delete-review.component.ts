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
  styleUrls: ['./delete-review.component.css','../../../../assets/css/local-design.css']
})
export class DeleteReviewComponent implements OnInit {
  reviewId!: number;
  freelancerId:number=0;
  constructor(
    private reviewService: ReviewService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.reviewId = +this.route.snapshot.paramMap.get('reviewId')!;
    this.freelancerId = +this.route.snapshot.paramMap.get('freelancerId')!;
  }

  deleteReview(): void {
    if (confirm('Are you sure you want to delete this review?')) {
      this.reviewService.deleteReview(this.reviewId).subscribe({
        next: () => {
          
        },
        error: (error) => {
          console.error('Error deleting review:', error);
        },
        complete:() =>{
          this.router.navigate(['/review/freelancer',this.freelancerId]);  
        }

      });
    }
  }
}
