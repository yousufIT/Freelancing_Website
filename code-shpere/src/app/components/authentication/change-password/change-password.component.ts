import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { PasswordData } from 'src/app/models/password-data';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-change-password',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css','../../../../assets/css/local-design.css']
})
export class ChangePasswordComponent {
  passwordData!:PasswordData
  currentPassword: string = '';
  newPassword: string = '';
  confirmPassword: string = '';
  errorMessage: string = '';
  successMessage: string = '';
  email:string=';';
  currentPasswordVisible = false;
  newPasswordVisible = false;
  confirmPasswordVisible = false;
  constructor(private authService: AuthService, private router: Router) {}

  changePassword() {
    if (this.newPassword !== this.confirmPassword) {
      this.errorMessage = "New password and confirmation do not match.";
      return;
    }
    this.passwordData ={
      email:this.email,
      currentPassword:this.currentPassword,
      newPassword:this.newPassword

    }

    this.authService.changePassword(this.passwordData).subscribe(
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
  toggleCurrentPasswordVisibility() {
    this.currentPasswordVisible = !this.currentPasswordVisible;
}

toggleNewPasswordVisibility() {
    this.newPasswordVisible = !this.newPasswordVisible;
}

toggleConfirmPasswordVisibility() {
    this.confirmPasswordVisible = !this.confirmPasswordVisible;
}
  cancel() {
    this.router.navigate(['/account/login']); 
  }
}
