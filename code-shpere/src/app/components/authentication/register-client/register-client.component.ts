import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ClientForCreate } from 'src/app/models/for-create/client-for-create';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-register-client',
  standalone: true,
  imports: [FormsModule,CommonModule,RouterLink],
  templateUrl: './register-client.component.html',
  styleUrls: ['./register-client.component.css']
})
export class RegisterClientComponent {
  user: ClientForCreate = {
    name: '',
    userName: '',
    email: '',
    passwordHash: '',
    role: 'client', // Default role
    rating: 0, // Default rating
    companyName: '',
    contactNumber: ''
  };

  errorMessage: string = '';

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService) {}

  register() {
    this.authService.registerClient(this.user).subscribe(
      (response) => {
        const token = response.token;
        const role=response.role;
        const id=response.id
        localStorage.setItem('token',token);
        localStorage.setItem('role',role);
        localStorage.setItem('User-Id',id.toString());

        this.authService.setLoggedInState(true);

        this.router.navigate(['/client/',id])
      },
      (error) => {
        this.errorMessage = 'Registration failed';
      }
    );
  }
}

