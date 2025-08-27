import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { environment } from '../../environments/environment';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection?: signalR.HubConnection;
  public newBid$ = new BehaviorSubject<any | null>(null);

  constructor(private auth: AuthService) {}

  private hubUrl(): string {
    // this returns "https://localhost:5001/hubs/notifications"
    return `${environment.apiUrl.replace(/\/api\/?$/,'')}/hubs/notifications`;
  }

  startConnection(): void {
    const token = this.auth.getToken() ?? '';
    console.log('SignalR: starting connection to', this.hubUrl(), 'with token present?', !!token);

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(this.hubUrl(), {
        accessTokenFactory: () => token,
        transport: signalR.HttpTransportType.WebSockets | signalR.HttpTransportType.LongPolling
      })
      .withAutomaticReconnect()
      .configureLogging(signalR.LogLevel.Information)
      .build();

    this.hubConnection.onreconnecting(error => {
      console.warn('SignalR: reconnecting', error);
    });

    this.hubConnection.onreconnected(connectionId => {
      console.log('SignalR: reconnected, connectionId=', connectionId);
    });

    this.hubConnection.onclose(error => {
      console.error('SignalR: connection closed', error);
    });

    this.hubConnection
      .start()
      .then(() => {
        console.log('SignalR: connected');
        this.registerHandlers();
      })
      .catch(err => {
        console.error('SignalR: start error', err);
      });
  }

  private registerHandlers(): void {
    if (!this.hubConnection) return;
    // log server messages generically
    this.hubConnection.on('NewBid', (payload: any) => {
      console.log('SignalR: NewBid received', payload);
      this.newBid$.next(payload);
    });
  }

  stopConnection(): Promise<void> | undefined {
    return this.hubConnection?.stop()
      .then(() => console.log('SignalR: stopped'))
      .catch(err => console.error('SignalR: stop error', err));
  }
}
