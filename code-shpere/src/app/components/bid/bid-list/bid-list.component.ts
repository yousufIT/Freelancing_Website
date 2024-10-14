import { Component, Input, OnInit } from '@angular/core';
import { BidService } from 'src/app/services/bid.service';
import { Bid } from 'src/app/models/bid';
import { DataWithPagination, PaginationMetaData } from 'src/app/models/data-with-pagination';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  standalone: true,
  imports: [CommonModule, RouterLink],
  selector: 'app-bid-list',
  templateUrl: './bid-list.component.html',
  styleUrls: ['./bid-list.component.css']
})
export class BidListComponent implements OnInit {
  projectId!: number; // Expecting the project ID to be passed from the parent
  bids: Bid[] = [];
  paginationMetaData: PaginationMetaData | undefined;
  currentPage: number = 1;
  pageSize: number = 10;
  totalPageCount: number = 0;
  auth:AuthService;
  constructor(private bidService: BidService,
    private authService:AuthService,
    private route: ActivatedRoute) {
      this.auth=authService;
    }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.projectId = +params['projectId']; // Retrieve project ID from route parameters
    });
    this.loadBids();
  }

  loadBids(): void {
    this.bidService.getBidsByProjectId(this.projectId, this.currentPage, this.pageSize).subscribe(data => {
      this.bids = data.items; 
      this.paginationMetaData = data.paginationMetaData;
      this.totalPageCount=this.paginationMetaData.totalPageCount;
    });
  }
  goToPage(page: number): void {
    this.currentPage = page;
    this.loadBids();
  }
}
