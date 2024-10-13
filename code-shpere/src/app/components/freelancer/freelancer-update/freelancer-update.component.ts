import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { FreelancerService } from '../../../services/freelancer.service';
import { Freelancer } from '../../../models/freelancer';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

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
    private freelancerService: FreelancerService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.params['id'];
    this.freelancerService.getFreelancerById(id).subscribe(data => {
      this.freelancer = data;
    });
  }

  updateFreelancer(): void {
    this.freelancerService.updateFreelancer(this.freelancer.id,this.freelancer).subscribe(() => {
      // Handle success (e.g., redirect or show a message)
    });
  }
}
