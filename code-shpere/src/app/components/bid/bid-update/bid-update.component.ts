import { Component, Input, OnInit } from '@angular/core';
import { BidService } from 'src/app/services/bid.service';
import { BidForCreate } from 'src/app/models/for-create/bid-for-create';
import { ActivatedRoute, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  standalone: true,
  imports: [FormsModule],
  selector: 'app-bid-update',
  templateUrl: './bid-update.component.html',
  styleUrls: ['./bid-update.component.css']
})
export class BidUpdateComponent implements OnInit {
  bidId!: number; // Expecting the bid ID to be passed from the parent
  bid: BidForCreate = { amount: 0, proposal: '' };
  projectId: number = 0;

  constructor(private bidService: BidService, private router: Router, private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.bidId = +params['bidId']; // Retrieve project ID from route parameters
    });
    this.loadBid();
  }

  loadBid(): void {
    this.bidService.getBidById(this.bidId).subscribe({
      next: (bid) => {
        this.bid = bid; // Fill the bid object with the returned bid data
        this.projectId = bid.projectId;
      },
      error: (err) => {
        console.error('Error loading bid:', err);
        // Optionally, show an error message to the user
      }
    });
  }
  
  

  updateBid(): void {
    this.bidService.updateBid(this.bidId, this.bid).subscribe(() => {
      // Redirect to project details or bid list after successful update
      this.router.navigate(['/projects', this.projectId, 'bids']);
    });
  }
}
