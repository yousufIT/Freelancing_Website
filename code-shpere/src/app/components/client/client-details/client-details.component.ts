import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { Client } from 'src/app/models/client';
import { ClientService } from 'src/app/services/client.service';

@Component({
  selector: 'app-client-details',
  standalone: true,
  imports: [FormsModule,CommonModule,RouterLink],
  templateUrl: './client-details.component.html',
  styleUrls: ['./client-details.component.css','../../../../assets/css/local-design.css']
})
export class ClientDetailsComponent implements OnInit {
  clientId: number | null = null;
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
    private clientService: ClientService
  ) {}

  ngOnInit(): void {
    this.clientId = +this.route.snapshot.paramMap.get('Id')!;
    if (this.clientId) {
      this.fetchClient();
    }
  }

  fetchClient() {
    if (this.clientId) {
      this.clientService.getClientById(this.clientId).subscribe( {
      next:(client)=>{
        this.client = client;
      },
      error:()=>{
        this.router.navigate(['/'])
      }
    });
    }
  }

}
