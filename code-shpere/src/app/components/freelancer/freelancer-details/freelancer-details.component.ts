import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FreelancerService } from '../../../services/freelancer.service';
import { Freelancer } from '../../../models/freelancer';

@Component({
  selector: 'app-freelancer-details',
  templateUrl: './freelancer-details.component.html',
  styleUrls: ['./freelancer-details.component.css']
})
export class FreelancerDetailsComponent implements OnInit {
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
}
