import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css','../../../../assets/css/local-design.css']
})
export class RegisterComponent {
  selectedRole: string = ''; // To store the user's choice (client or freelancer)

  constructor(private router: Router) {}

  onSubmit() {
    if (this.selectedRole === 'client') {
      this.router.navigate(['/account/register/client']);
    } else if (this.selectedRole === 'freelancer') {
      this.router.navigate(['/account/register/freelancer']);
    }
  }

  goToLogin() {
    this.router.navigate(['/account/login']);
  }
}
