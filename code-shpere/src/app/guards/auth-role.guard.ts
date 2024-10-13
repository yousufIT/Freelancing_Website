import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthRoleGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const expectedRoles = route.data['roles'];
    const userRole = this.authService.getUserRole();

    if (expectedRoles.includes(userRole)) {
      return true; 
    } else {
      alert('you are unauthorized')
      if(userRole=='Client')
        this.router.navigate(['/client/',this.authService.getUserId()]);
      else
        this.router.navigate(['/freelancer/',this.authService.getUserId()]);

      return false;
    }
  }
}
