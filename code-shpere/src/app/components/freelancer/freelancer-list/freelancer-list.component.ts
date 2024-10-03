import { Component, OnInit } from '@angular/core';
import { FreelancerService } from '../../../services/freelancer.service';
import { Freelancer } from '../../../models/freelancer';

@Component({
  selector: 'app-freelancer-list',
  templateUrl: './freelancer-list.component.html',
  styleUrls: ['./freelancer-list.component.css']
})
export class FreelancerListComponent implements OnInit {
  freelancers: Freelancer[] = [];

  constructor(private freelancerService: FreelancerService) {}

  ngOnInit(): void {
    this.freelancerService.getAllFreelancers().subscribe(data => {
      this.freelancers = data;
    });
  }
}
