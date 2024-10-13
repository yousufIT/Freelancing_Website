import { Component, Input, OnInit } from '@angular/core';
import { PortfolioItemForCreate } from 'src/app/models/for-create/portfolio-item-for-create';
import { ProfileService } from 'src/app/services/profile.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-portfolio-create',
  templateUrl: './portfolio-create.component.html',
  styleUrls: ['./portfolio-create.component.css'],
  standalone: true,
  imports: [FormsModule]
})
export class PortfolioCreateComponent implements OnInit {
  profileId!: number;
  portfolioItem: PortfolioItemForCreate = { title: '', description: '', imageUrl: '' };
  isEdit = false;
  portfolioItemId: number | undefined;

  constructor(private profileService: ProfileService, private router: Router, private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.profileId = +params['profileId'];
      if (params['portfolioItemId']) {
        this.isEdit = true;
        this.portfolioItemId = +params['portfolioItemId'];
        this.loadPortfolioItem(this.portfolioItemId);
      }
    });
  }

  loadPortfolioItem(id: number): void {
    this.profileService.getPortfolioItemById(id).subscribe((data)=>{
      this.portfolioItem = data;
    });
  }

  onSubmit(): void {
    if (this.isEdit && this.portfolioItemId) {
      this.profileService.updatePortfolioItem(this.portfolioItemId, this.portfolioItem)
        .subscribe(() => this.router.navigate(['/profiles', this.profileId,'portfolio']));
    } else {
      this.profileService.createPortfolioItem(this.profileId, this.portfolioItem)
        .subscribe(() => this.router.navigate(['/profiles', this.profileId, 'portfolio']));
    }
  }
}
