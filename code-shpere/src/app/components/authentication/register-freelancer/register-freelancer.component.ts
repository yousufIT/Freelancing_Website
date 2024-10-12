import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { FreelancerForCreate } from 'src/app/models/for-create/freelancer-for-create';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-register-freelancer',
  standalone: true,
  imports: [FormsModule,CommonModule,RouterLink],
  templateUrl: './register-freelancer.component.html',
  styleUrls: ['./register-freelancer.component.css','../../../../assets/css/local-design.css']
})
export class RegisterFreelancerComponent {
  user: FreelancerForCreate = {
    profile: {
      bio: ''
    },
    name: '',
    rating: 0, // Default rating
    userName: '',
    role: 'freelancer', // Default role
    passwordHash: '',
    email: '',
    hourlyRate: 0 // Default hourly rate
  };

  errorMessage: string = '';

  constructor(private authService: AuthService) {}

  register() {
    this.authService.registerFreelancer(this.user).subscribe(
      (freelancer) => {
        alert('Freelancer registered successfully');
      },
      (error) => {
        this.errorMessage = 'Registration failed';
      }
    );
  }
}

