import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router, RouterModule, RouterOutlet } from '@angular/router';
import noUiSlider from "nouislider";
import { routes } from 'src/app/app.routes';
import { CollapseModule } from 'ngx-bootstrap/collapse';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    RouterOutlet,
    RouterModule,
    CommonModule,
    CollapseModule
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  

  constructor() {}

  

}
