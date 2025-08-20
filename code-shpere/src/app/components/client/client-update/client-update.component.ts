import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ClientService } from 'src/app/services/client.service';
import { Client } from 'src/app/models/client'; // Adjust the path according to your project structure
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-client-update',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './client-update.component.html',
  styleUrls: ['./client-update.component.css']
})
export class ClientUpdateComponent implements OnInit {
  clientId: number | undefined; // Store the client ID
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
    private clientService: ClientService,
    private router: Router,
    private authService:AuthService
  ) {
    
  }

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

  updateClient() {
    this.client.passwordHash = '';
    this.clientService.updateClient(this.clientId!, this.client).subscribe(() => {
      this.router.navigate(['/client/',this.clientId]); // Redirect to clients list after successful update
    });
  }
}
