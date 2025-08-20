import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { FreelancerForCreate } from 'src/app/models/for-create/freelancer-for-create';
import { FreelancerService } from 'src/app/services/freelancer.service';
import { Freelancer } from 'src/app/models/freelancer';
import { AuthService } from 'src/app/services/auth.service';
import { ProfileService } from 'src/app/services/profile.service';
import { PortfolioItem } from 'src/app/models/portfolio-item';

@Component({
  selector: 'app-manage-freelancer',
  standalone: true,
  imports: [FormsModule, CommonModule, RouterLink],
  templateUrl: './manage-freelancer.component.html',
  styleUrls: ['./manage-freelancer.component.css']
})
export class ManageFreelancerComponent implements OnInit {
  freelancerId: number | null = null;
  portfolioItems: PortfolioItem[] = [];
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
    private freelancerService: FreelancerService,
    private authService: AuthService,
    private profileService: ProfileService
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


  editFreelancer() {
    if (this.freelancerId) {
      this.router.navigate(['/freelancer/update/', this.freelancerId]);
    }
  }

  logout() {
    this.authService.logout(); 
    this.router.navigate(['/account/login']); 
  }

  addSkills():void {
    if(this.freelancerId){
      this.router.navigate(['/skills/freelancer/add',this.freelancerId]);
    }
  }


  
  loadPortfolioItems(): void {
    if(this.freelancerId){
      this.profileService.getPortfolioItems(this.freelancerId, 1, 10).subscribe(data => {
        this.portfolioItems = data.items;
      });
    }
  }

  onCreatePortfolioItem(): void {
    this.router.navigate([`/freelancer/${this.freelancerId}/profiles/${this.freelancer.profileId}/portfolio`]);
  }

  onEditPortfolioItem(portfolioItemId: number): void {
    this.router.navigate([`/profiles/${this.freelancerId}/portfolio/${portfolioItemId}/edit`]);
  }

  onDeletePortfolioItem(portfolioItemId: number): void {
    this.profileService.deletePortfolioItem(portfolioItemId).subscribe(() => {
      this.loadPortfolioItems(); // Reload the list after deletion
    });
  }

}
