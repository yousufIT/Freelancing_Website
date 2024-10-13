import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { FreelancerService } from '../../../services/freelancer.service';
import { Freelancer } from '../../../models/freelancer';
import { FreelancerForCreate } from 'src/app/models/for-create/freelancer-for-create';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-freelancer-details',
  standalone:true,
  imports:[FormsModule,CommonModule,RouterLink],
  templateUrl: './freelancer-details.component.html',
  styleUrls: ['./freelancer-details.component.css','../../../../assets/css/local-design.css']
})
export class FreelancerDetailsComponent implements OnInit {
  freelancerId: number | null = null;
  
  freelancer: Freelancer = {
    id: 0,
    name: '',
    rating: 0,
    userName: '',
    role: '',
    passwordHash: '',
    email: '',
    hourlyRate: 0,
    bids: [],
    reviewsReceived: [],
    completedProjects: [],
    profileId: 0,
    profile: {
        id: 0,
        freelancerId: 0,
        freelancer: {} as Freelancer,  
        skills: [],
        portfolio: [],              
        portfolioItems: [],         
        bio: ''
    }
};

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private freelancerService: FreelancerService
  ) {}

  ngOnInit(): void {
    this.freelancerId = +this.route.snapshot.paramMap.get('Id')!;
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
}
