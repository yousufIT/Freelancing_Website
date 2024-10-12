import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule,CommonModule,RouterLink],
  templateUrl: './login.component.html',
  // stylesUrl: 
  styleUrls:['./login.component.css','../../../../assets/css/local-design.css']
})
export class LoginComponent {
  email: string = '';
  password: string = '';
  errorMessage: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  login() {
    const credentials = { email: this.email, password: this.password };
    this.authService.login(credentials).subscribe(
      (user) => {
        // Handle successful login
        this.router.navigate(['/dashboard']);
      },
      (error) => {
        // Handle login error
        this.errorMessage = 'Invalid email or password';
      }
    );
  }
}
