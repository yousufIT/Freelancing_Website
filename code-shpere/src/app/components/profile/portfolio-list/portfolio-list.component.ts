import { Component, Input, OnInit } from '@angular/core';
import { ProfileService } from 'src/app/services/profile.service';
import { PortfolioItem } from 'src/app/models/portfolio-item';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { PaginationMetaData } from 'src/app/models/data-with-pagination';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  standalone: true,
  imports: [RouterLink,FormsModule,CommonModule],
  selector: 'app-portfolio-list',
  templateUrl: './portfolio-list.component.html',
  styleUrls: ['./portfolio-list.component.css']
})
export class PortfolioListComponent implements OnInit {
  profileId!: number; 
  freelancerId!: number;
  portfolioItems: PortfolioItem[] = [];
  pageNumber = 1;
  pageSize = 5; // Items per page
  paginationMetaData: PaginationMetaData | undefined;

  constructor(private profileService: ProfileService, private route: ActivatedRoute, public authService: AuthService) {}

  ngOnInit(): void {
    // Get Profile ID from parent or route params
    if (!this.profileId) {
      this.route.params.subscribe(params => {
        this.profileId = +params['profileId'];
        this.freelancerId = +params['freelancerId'];
      });
    }

    this.loadPortfolioItems();
  }

  loadPortfolioItems(): void {
    this.profileService.getPortfolioItems(this.profileId, this.pageNumber, this.pageSize)
      .subscribe(data => {
        this.portfolioItems = data.items;
        this.paginationMetaData = data.paginationMetaData;
      });
  }

  onDelete(id: number): void {
    this.profileService.deletePortfolioItem(id).subscribe(() => {
      this.portfolioItems = this.portfolioItems.filter(item => item.id !== id);
    });
  }
}
