import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { FreelancerForCreate } from 'src/app/models/for-create/freelancer-for-create';
import { FreelancerService } from 'src/app/services/freelancer.service';

@Component({
  selector: 'app-manage-freelancer',
  standalone: true,
  imports: [FormsModule,CommonModule,RouterLink],
  templateUrl: './manage-freelancer.component.html',
  styleUrl: './manage-freelancer.component.css'
})
export class ManageFreelancerComponent implements OnInit {
  freelancerId: number | null = null;
  freelancer: FreelancerForCreate = {
    name: '',
    userName: '',
    email: '',
    passwordHash: '',
    role: '',
    rating: 0,
    hourlyRate:0,
    profile:{
      bio:''
    }
  };

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private freelancerService: FreelancerService
  ) {}

  ngOnInit(): void {
    this.freelancerId = +this.route.snapshot.paramMap.get('freelancerId')!;
    if (this.freelancerId) {
      this.fetchfreelancer();
    }

    // Form validation
    this.setupValidation();
  }

  fetchfreelancer() {
    if (this.freelancerId) {
      this.freelancerService.getFreelancerById(this.freelancerId).subscribe(freelancer => {
        this.freelancer = freelancer;
      });
    }
  }

  submitfreelancer() {
    if (this.freelancerId) {
      this.freelancerService.updateFreelancer(this.freelancerId, this.freelancer).subscribe(() => {
        // Handle successful update (e.g., navigate back or show a success message)
        this.router.navigate(['/freelancers']); // Adjust the path as necessary
      });
    } else {
      this.freelancerService.createFreelancer(this.freelancer).subscribe(() => {
        // Handle successful creation
        this.router.navigate(['/freelancers']); // Adjust the path as necessary
      });
    }
  }

  deletefreelancer() {
    if (this.freelancerId) {
      this.freelancerService.deleteFreelancer(this.freelancerId).subscribe(() => {
        // Handle successful deletion
        this.router.navigate(['/freelancers']); // Adjust the path as necessary
      });
    }
  }

  setupValidation() {
    const form = document.querySelector('form');
    if (form) {
      form.addEventListener('submit', function (event) {
        if (!form.checkValidity()) {
          event.preventDefault();
          event.stopPropagation();
        }
        form.classList.add('was-validated');
      });
    }
  }
}