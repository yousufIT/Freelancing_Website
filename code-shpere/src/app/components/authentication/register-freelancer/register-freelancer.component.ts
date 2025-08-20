import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
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
    rating: 0, 
    userName: '',
    role: 'freelancer', 
    passwordHash: '',
    email: '',
  };
  errorMessage: string = '';

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService) {}

  register() {
    this.authService.registerFreelancer(this.user).subscribe(
      (response) => {
        const token = response.token;
        const role=response.role;
        const id=response.id
        localStorage.setItem('token',token);
        localStorage.setItem('role',role);
        localStorage.setItem('User-Id',id.toString());
        this.router.navigate(['/freelancer/',id])

      },
      (error) => {
        this.errorMessage = 'Registration failed';
      }
    );
  }
}

