import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ClientForCreate } from 'src/app/models/for-create/client-for-create';
import { ClientService } from 'src/app/services/client.service';

@Component({
  selector: 'app-manage-client',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './manage-client.component.html',
  styleUrl: './manage-client.component.css'
})
export class ManageClientComponent implements OnInit {
  clientId: number | null = null;
  client: ClientForCreate = {
    name: '',
    userName: '',
    email: '',
    passwordHash: '',
    role: '',
    rating: 0,
    companyName: '',
    contactNumber: ''
  };

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private clientService: ClientService
  ) {}

  ngOnInit(): void {
    this.clientId = +this.route.snapshot.paramMap.get('clientId')!;
    if (this.clientId) {
      this.fetchClient();
    }

    // Form validation
    this.setupValidation();
  }

  fetchClient() {
    if (this.clientId) {
      this.clientService.getClientById(this.clientId).subscribe(client => {
        this.client = client;
      });
    }
  }

  submitClient() {
    if (this.clientId) {
      this.clientService.updateClient(this.clientId, this.client).subscribe(() => {
        // Handle successful update (e.g., navigate back or show a success message)
        this.router.navigate(['/clients']); // Adjust the path as necessary
      });
    } else {
      this.clientService.createClient(this.client).subscribe(() => {
        // Handle successful creation
        this.router.navigate(['/clients']); // Adjust the path as necessary
      });
    }
  }

  deleteClient() {
    if (this.clientId) {
      this.clientService.deleteClient(this.clientId).subscribe(() => {
        // Handle successful deletion
        this.router.navigate(['/clients']); // Adjust the path as necessary
      });
    }
  }

  setupValidation() {
    const form = document.querySelector('form');
    if (form) {
      form.addEventListener('submit', function (event) {
        if (!form.checkValidity()) {
          event.preventDefault();
          event.stopPropagation();
        }
        form.classList.add('was-validated');
      });
    }
  }
}