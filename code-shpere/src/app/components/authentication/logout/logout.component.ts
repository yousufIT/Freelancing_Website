import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-logout',
  standalone: true,
  imports: [],
  templateUrl: './logout.component.html',
  styleUrl: './logout.component.css'
})
export class LogoutComponent {

  constructor(private authService: AuthService, private router: Router) {}

  logout() {
    this.authService.logout().subscribe(
      () => {
        // On successful logout, redirect to login page
        this.router.navigate(['/login']);
      },
      (error) => {
        console.error('Logout failed', error);
      }
    );
  }
}
