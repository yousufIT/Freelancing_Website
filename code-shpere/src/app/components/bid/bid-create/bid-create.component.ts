import { Component, Input, OnInit } from '@angular/core';
import { BidService } from 'src/app/services/bid.service';
import { BidForCreate } from 'src/app/models/for-create/bid-for-create';
import { ActivatedRoute, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  standalone: true,
  imports: [FormsModule],
  selector: 'app-bid-create',
  templateUrl: './bid-create.component.html',
  styleUrls: ['./bid-create.component.css']
})
export class BidCreateComponent implements OnInit {
  projectId!: number; // Expecting the project ID to be passed from the parent
  bid: BidForCreate = { amount: 0, proposal: '',freelancerId: 0, projectId: 0 };

  constructor(private bidService: BidService, private router: Router, private route: ActivatedRoute, private authService: AuthService) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.projectId = +params['projectId']; // Retrieve project ID from route parameters
    });
  }

  createBid(): void {
    this.bid.freelancerId = this.authService.getUserId();
    this.bid.projectId = this.projectId;
    this.bidService.createBid(this.authService.getUserId(), this.projectId, this.bid).subscribe( {
      next:() =>{
      this.router.navigate(['/project', this.projectId]);
    },
    error: (err) => {
      if (err.status === 400 && err.error.message === "You have already placed a bid on this project.") {
        alert("Error: You have already placed a bid on this project.");
        this.router.navigate(['/projects',this.projectId,'bids']);
      } else {
        console.log("An error occurred. Please try again.");
      }
    }
    });
  }
}
