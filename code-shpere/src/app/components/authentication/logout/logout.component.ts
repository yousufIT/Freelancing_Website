import { Component, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { LoginComponent } from '../login/login.component';

@Component({
  selector: 'app-logout',
  standalone: true,
  imports: [RouterLink,LoginComponent],
  templateUrl: './logout.component.html',
  styleUrl: './logout.component.css'
})
export class LogoutComponent implements OnInit {

  constructor(private authService: AuthService, private router: Router) {}
  ngOnInit(): void {
    this.logout();
  }

  logout() {
    this.authService.logout().subscribe({
      next:() =>{
        this.router.navigate(['/account/login']);
      },
      error(err) {
          console.log(err);
          
      },
    }
      
    )
    
  }
}
