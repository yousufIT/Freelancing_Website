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
  styleUrls:['./login.component.css']
})
export class LoginComponent {
  email: string = '';
  password: string = '';
  errorMessage: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  login() {
    const loginData = { email: this.email, password: this.password };
    this.authService.login(loginData).subscribe(
      (response) => {
        const token = response.token;
        const role=response.role;
        const id=response.id
        localStorage.setItem('token',token);
        localStorage.setItem('role',role);
        localStorage.setItem('User-Id',id.toString());
        if(response.role=='Freelancer')
          this.router.navigate(['/freelancer/',id])
        else 
          this.router.navigate(['/client/',id])


      },
      (error) => {
        this.errorMessage = 'Invalid email or password';
      }
    );
  }
}
