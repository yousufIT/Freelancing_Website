import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { ClientForCreate } from 'src/app/models/for-create/client-for-create';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-register-client',
  standalone: true,
  imports: [FormsModule,CommonModule,RouterLink],
  templateUrl: './register-client.component.html',
  styleUrls: ['./register-client.component.css','../../../../assets/css/local-design.css']
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

  constructor(private authService: AuthService) {}

  register() {
    this.authService.registerClient(this.user).subscribe(
      (client) => {
        alert('Client registered successfully');
      },
      (error) => {
        this.errorMessage = 'Registration failed';
      }
    );
  }
}

