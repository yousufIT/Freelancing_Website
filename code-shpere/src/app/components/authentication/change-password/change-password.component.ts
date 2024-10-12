import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-change-password',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css','../../../../assets/css/local-design.css']
})
export class ChangePasswordComponent {
  currentPassword: string = '';
  newPassword: string = '';
  confirmPassword: string = '';
  errorMessage: string = '';
  successMessage: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  changePassword() {
    if (this.newPassword !== this.confirmPassword) {
      this.errorMessage = "New password and confirmation do not match.";
      return;
    }

    const credentials = {
      currentPassword: this.currentPassword,
      newPassword: this.newPassword
    };

    this.authService.changePassword(credentials).subscribe(
      (response) => {
        this.successMessage = "Password changed successfully.";
        this.errorMessage = '';
      },
      (error) => {
        this.errorMessage = "Failed to change password.";
        this.successMessage = '';
      }
    );
  }

  cancel() {
    this.router.navigate(['/account/login']); 
  }
}
