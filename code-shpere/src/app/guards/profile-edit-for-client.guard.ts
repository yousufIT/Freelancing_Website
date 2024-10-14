import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root',
})
export class ProfileEditForClientGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot): boolean {
    const userId = this.authService.getUserId();
    const routeUserId = route.paramMap.get('Id');
    if (routeUserId !== null && +routeUserId === userId) {
      return true;
    }
    this.router.navigate(['/client-details/',routeUserId])

    return false;
    
  }
}
