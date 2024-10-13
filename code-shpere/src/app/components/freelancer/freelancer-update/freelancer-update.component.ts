import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { FreelancerService } from '../../../services/freelancer.service';
import { Freelancer } from '../../../models/freelancer';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-freelancer-update',
  standalone:true,
  imports:[FormsModule,CommonModule,RouterLink],
  templateUrl: './freelancer-update.component.html',
  styleUrls: ['./freelancer-update.component.css','../../../../assets/css/local-design.css']
})
export class FreelancerUpdateComponent implements OnInit {
  freelancer!: Freelancer;

  constructor(
    private route: ActivatedRoute,
    private freelancerService: FreelancerService,
    private router: Router,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    const id = +this.route.snapshot.paramMap.get('Id')!;
    if(id){
      this.freelancerService.getFreelancerById(id).subscribe(data => {
        this.freelancer = data;
      });
    }
  }

  updateFreelancer(): void {
    this.freelancerService.updateFreelancer(this.freelancer.id,this.freelancer).subscribe(() => {
      this.router.navigate([`/${this.authService.getUserRole().toLowerCase()}`,this.freelancer.id]);
    });
  }
}
