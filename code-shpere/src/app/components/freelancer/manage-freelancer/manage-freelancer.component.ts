import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { FreelancerForCreate } from 'src/app/models/for-create/freelancer-for-create';
import { FreelancerService } from 'src/app/services/freelancer.service';
import { Freelancer } from 'src/app/models/freelancer';

@Component({
  selector: 'app-manage-freelancer',
  standalone: true,
  imports: [FormsModule, CommonModule, RouterLink],
  templateUrl: './manage-freelancer.component.html',
  styleUrls: ['./manage-freelancer.component.css', '../../../../assets/css/local-design.css']
})
export class ManageFreelancerComponent implements OnInit {
  freelancerId: number | null = null;
  freelancerForCreate: FreelancerForCreate = {
    name: '',
    userName: '',
    email: '',
    passwordHash: '',
    role: '',
    rating: 0,
    hourlyRate: 0,
    profile: {
      bio: ''
    }
  };
  freelancer!: Freelancer;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private freelancerService: FreelancerService
  ) {}

  ngOnInit(): void {
    this.freelancerId = +this.route.snapshot.paramMap.get('freelancerId')!;
    if (this.freelancerId) {
      this.fetchFreelancer();
    }
  }

  fetchFreelancer() {
    if (this.freelancerId) {
      this.freelancerService.getFreelancerById(this.freelancerId).subscribe(freelancer => {
        this.freelancer = freelancer;
      });
    }
  }

  submitFreelancer() {
    if (this.freelancerId) {
      this.freelancerService.updateFreelancer(this.freelancerId, this.freelancerForCreate).subscribe(() => {
        this.router.navigate(['/freelancers']); // Adjust the path as necessary
      });
    } else {
      this.freelancerService.createFreelancer(this.freelancer).subscribe(() => {
        this.router.navigate(['/freelancers']); // Adjust the path as necessary
      });
    }
  }

  editFreelancer() {
    if (this.freelancerId) {
      this.freelancerService.updateFreelancer(this.freelancerId, this.freelancer).subscribe(() => {
        this.router.navigate(['/freelancers']); // Adjust the path as necessary
      });
    }
  }

  deleteFreelancer() {
    if (this.freelancerId) {
      this.freelancerService.deleteFreelancer(this.freelancerId).subscribe(() => {
        this.router.navigate(['/freelancers']); // Adjust the path as necessary
      });
    }
  }
}
