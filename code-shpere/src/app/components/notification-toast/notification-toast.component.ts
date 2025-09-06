import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SignalRService } from 'src/app/services/signalr.service';
import { AuthService } from 'src/app/services/auth.service';
import { RouterModule } from '@angular/router';

interface OwnerBidPayload {
  projectId: number;
  projectTitle: string;
  bidId: number;
  amount: number;
  freelancerName: string;
}

@Component({
  selector: 'app-notification-toast',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './notification-toast.component.html',
  styleUrls: ['./notification-toast.component.css']
})
export class NotificationToastComponent implements OnInit {
  toasts: OwnerBidPayload[] = [];

  constructor(private signalR: SignalRService, private auth: AuthService) {}

  ngOnInit(): void {
    this.signalR.newOwnerBid$.subscribe(payload => {
      if (!payload) return;
      // push toasts to array to render; keep max, e.g. 4
      this.toasts.unshift(payload);
      if (this.toasts.length > 5) this.toasts.pop();

      // automatically remove after timeout
      setTimeout(() => {
        this.toasts = this.toasts.filter(t => t !== payload);
      }, 10000); // 10 seconds
    });
  }

  close(toast: OwnerBidPayload) {
    this.toasts = this.toasts.filter(t => t !== toast);
  }
}
