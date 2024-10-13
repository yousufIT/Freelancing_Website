import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router, RouterModule, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    RouterOutlet,
    RouterModule,
    CommonModule
  ],
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  isSigned: boolean = false;
  isNavbarCollapsed: boolean = true; // Initial state for the navbar

  constructor() {}

  ngOnInit(): void {
    this.isSigned = !!localStorage.getItem('User-Id');
  }
}
