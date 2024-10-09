import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-portfolio-item',
  templateUrl: './portfolio-item.component.html',
  imports: [FormsModule,CommonModule]
})
export class PortfolioItemComponent  {
  portfolioItems: any[] = [];

//   constructor(private portfolioService: PortfolioService) {}

//   ngOnInit(): void {
//     this.portfolioService.getPortfolioItems().subscribe((data) => {
//       this.portfolioItems = data;
//     });
//   }
}
