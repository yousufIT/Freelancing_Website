import { Component } from '@angular/core';
import { FreelancerService } from '../../../services/freelancer.service';
import { Freelancer } from '../../../models/freelancer';

@Component({
  selector: 'app-freelancer-create',
  templateUrl: './freelancer-create.component.html',
  styleUrls: ['./freelancer-create.component.css']
})
export class FreelancerCreateComponent {
  freelancer!: Freelancer;

  constructor(private freelancerService: FreelancerService) {}

  createFreelancer(): void {
    this.freelancerService.createFreelancer(this.freelancer).subscribe(() => {});
  }
}
