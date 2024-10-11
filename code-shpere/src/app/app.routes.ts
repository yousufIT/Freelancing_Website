import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProjectListComponent } from './project-list/project-list.component';
import { ProjectDetailComponent } from './project-detail/project-detail.component';
import { CreateProjectComponent } from './create-project/create-project.component';
import { PortfolioItemComponent } from './portfolio-item/portfolio-item.component';
import { ReviewComponent } from './review/review.component';
import { AuthGuard } from './guards/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: '/projects', pathMatch: 'full' },
  { path: 'projects', component: ProjectListComponent },
  { path: 'project/:id', component: ProjectDetailComponent },
  { path: 'create-project', component: CreateProjectComponent, canActivate: [AuthGuard] },
  { path: 'portfolio', component: PortfolioItemComponent },
  { path: 'reviews', component: ReviewComponent },

];


