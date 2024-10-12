import { Component, OnInit } from '@angular/core';
import { ReviewService } from 'src/app/services/review.service'; 
import { Review } from 'src/app/models/review';
import { DataWithPagination, PaginationMetaData } from 'src/app/models/data-with-pagination';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { Client } from 'src/app/models/client';
import { ClientService } from 'src/app/services/client.service';

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
  
  constructor(private route: ActivatedRoute,
    private reviewService: ReviewService,
    private clientService:ClientService) {
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

  // getClientName(clientId:number):string{
  //   let name: string = '';
  //   this.clientService.getClientById(clientId).subscribe({
  //     next:(data:Client)=>{
  //       name=data.name
  //     },
  //     error: (error) => {
  //       console.error('Error fetching client:', error);
  //     }
  //   });
  //   return name;
  // }
}
