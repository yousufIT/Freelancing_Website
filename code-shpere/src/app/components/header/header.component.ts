// src/app/components/header/header.component.ts
import { CommonModule } from '@angular/common';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterModule, CommonModule],
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit, OnDestroy {
  isSigned = false;
  isNavbarCollapsed = true;
  private sub!: Subscription;

  constructor(public auth: AuthService, private router: Router) {}

  ngOnInit(): void {
    // reactive subscription — header updates immediately on login/logout
    this.sub = this.auth.isLoggedIn$.subscribe(v => {
      this.isSigned = v;
    });
  }

  ngOnDestroy(): void {
    this.sub?.unsubscribe();
  }

  logout(): void {
    // clear client state and call logout API (UI updates immediately)
    this.auth.logout().subscribe({
      next: () => {
        // auth.clearAuth() already called inside logout(); ensure navigation
        this.router.navigate(['/login']);
      },
      error: () => {
        // even if API fails, ensure client cleared and navigate
        this.auth.clearAuth();
        this.router.navigate(['/login']);
      }
    });
  }
}
