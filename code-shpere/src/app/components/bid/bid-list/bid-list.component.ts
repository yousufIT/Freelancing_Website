import { Component, Input, OnInit } from '@angular/core';
import { BidService } from 'src/app/services/bid.service';
import { Bid } from 'src/app/models/bid';
import { DataWithPagination, PaginationMetaData } from 'src/app/models/data-with-pagination';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';

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
  pageNumber = 1;
  pageSize = 5; // Number of bids per page
  paginationMetaData: PaginationMetaData | undefined;

  constructor(private bidService: BidService, private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.projectId = +params['projectId']; // Retrieve project ID from route parameters
    });
    this.loadBids();
  }

  loadBids(): void {
    this.bidService.getBidsByProjectId(this.projectId, this.pageNumber, this.pageSize).subscribe(data => {
      this.bids = data.items; 
      this.paginationMetaData = data.paginationMetaData;
    });
  }
}
