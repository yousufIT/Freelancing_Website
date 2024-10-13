import { CommonModule } from '@angular/common';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, RouterModule, RouterOutlet } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    RouterOutlet,
    RouterModule,
    CommonModule
  ],
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit, OnDestroy {
  isSigned: boolean = false;
  isNavbarCollapsed: boolean = true; // Initial state for the navbar
  private storageListener!: () => void;
  auth: AuthService;

  constructor(private authService: AuthService) {
    this.auth = authService;
  }

  ngOnInit(): void {
    // Initialize isSigned status
    this.isSigned = this.authService.isLoggedIn();

    // Listen for changes in localStorage
    this.storageListener = this.detectLocalStorageChanges.bind(this);

    // Add event listeners for both native storage events (cross-tab) and custom events (same-tab)
    window.addEventListener('storage', this.storageListener);
    window.addEventListener('localStorageChanged', this.storageListener);
  }

  detectLocalStorageChanges(): void {
    // Update isSigned status whenever localStorage changes
    this.isSigned = this.authService.isLoggedIn();
    setTimeout(() => {
      this.isSigned = this.authService.isLoggedIn();
    }, 1000);
  }

  ngOnDestroy(): void {
    // Clean up the event listeners when the component is destroyed
    window.removeEventListener('storage', this.storageListener);
    window.removeEventListener('localStorageChanged', this.storageListener);
  }

  logout(): void {
    this.authService.logout().subscribe(() => {
    });
  }
}
