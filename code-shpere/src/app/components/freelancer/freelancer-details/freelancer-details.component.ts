import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { FreelancerService } from '../../../services/freelancer.service';
import { Freelancer } from '../../../models/freelancer';
import { FreelancerForCreate } from 'src/app/models/for-create/freelancer-for-create';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ProfileService } from 'src/app/services/profile.service';
import { PortfolioItem } from 'src/app/models/portfolio-item';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-freelancer-details',
  standalone:true,
  imports:[FormsModule,CommonModule,RouterLink],
  templateUrl: './freelancer-details.component.html',
  styleUrls: ['./freelancer-details.component.css']
})
export class FreelancerDetailsComponent implements OnInit {
  freelancerId: number | null = null;
  portfolioItems: PortfolioItem[] = [];
  freelancer: Freelancer = {
    id: 0,
    name: '',
    rating: 0,
    userName: '',
    role: '',
    passwordHash: '',
    email: '',
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
auth:AuthService;
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private freelancerService: FreelancerService,
    private profileService: ProfileService,
    private authService: AuthService
  ) {
    this.auth = authService;
  }

  ngOnInit(): void {
    this.freelancerId = +this.route.snapshot.paramMap.get('Id')!;
    if (this.freelancerId) {
      this.fetchFreelancer();
    }
  }

  fetchFreelancer() {
    if (this.freelancerId) {
      this.freelancerService.getFreelancerById(this.freelancerId).subscribe({
        next:(freelancer)=>{
          this.freelancer = freelancer;
        },
        error:()=>{
          this.router.navigate(['/'])
        }
      });
    }
  }

  loadPortfolioItems(): void {
    if(this.freelancerId){
      this.profileService.getPortfolioItems(this.freelancerId, 1, 10).subscribe(data => {
        this.portfolioItems = data.items;
      });
    }
  }
  onCreateReviewFromCLient():void{
    this.router.navigate([`/review/client`,this.auth.getUserId(),`freelancer`,this.freelancerId]);
  }
  onCreatePortfolioItem(): void {
    this.router.navigate([`/freelancer/${this.freelancerId}/profiles/${this.freelancer.profileId}/portfolio`]);
  }
}
