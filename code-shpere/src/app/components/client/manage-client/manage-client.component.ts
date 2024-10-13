import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { Client } from 'src/app/models/client';
import { ClientForCreate } from 'src/app/models/for-create/client-for-create';
import { AuthService } from 'src/app/services/auth.service';
import { ClientService } from 'src/app/services/client.service';

@Component({
  selector: 'app-manage-client',
  standalone: true,
  imports: [FormsModule, CommonModule,RouterLink],
  templateUrl: './manage-client.component.html',
  styleUrls: ['./manage-client.component.css','../../../../assets/css/local-design.css']
})
export class ManageClientComponent implements OnInit {
  clientId: number | null = null;
  clientForCreate: ClientForCreate = {
    name: '',
    userName: '',
    email: '',
    passwordHash: '',
    role: '',
    rating: 0,
    companyName: '',
    contactNumber: ''
  };
  client: Client = {
    id: 0,
    name: '',
    userName: '',
    email: '',
    passwordHash: '',
    role: '',
    rating: 0,
    companyName: '',
    contactNumber: '',
    reviewsGiven: [],
    postedProjects: []
  };
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private clientService: ClientService,
    private authService:AuthService
  ) {}

  ngOnInit(): void {
    this.clientId = +this.route.snapshot.paramMap.get('Id')!;
    if (this.clientId) {
      this.fetchClient();
    }
  }

  fetchClient() {
    if (this.clientId) {
      this.clientService.getClientById(this.clientId).subscribe(client => {
        this.client = client;
      });
    }
  }


  
  editClient() {
        this.router.navigate(['/client/update/',this.authService.getUserId()]); 
  }
  logout() {
    this.authService.logout(); 
}

}
