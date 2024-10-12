import { Component, Input, OnInit } from '@angular/core';
import { BidService } from 'src/app/services/bid.service';
import { BidForCreate } from 'src/app/models/for-create/bid-for-create';
import { ActivatedRoute, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  standalone: true,
  imports: [FormsModule],
  selector: 'app-bid-create',
  templateUrl: './bid-create.component.html',
  styleUrls: ['./bid-create.component.css']
})
export class BidCreateComponent implements OnInit {
  projectId!: number; // Expecting the project ID to be passed from the parent
  bid: BidForCreate = { amount: 0, proposal: '' };

  constructor(private bidService: BidService, private router: Router, private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.projectId = +params['projectId']; // Retrieve project ID from route parameters
    });
  }

  createBid(): void {
    this.bidService.createBid(3, this.projectId, this.bid).subscribe(() => {
      // Redirect to project details or bid list after successful creation
      this.router.navigate(['/project', this.projectId]);
    });
  }
}
