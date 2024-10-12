import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProjectListComponent } from './project-list/project-list.component';
import { ProjectDetailComponent } from './project-detail/project-detail.component';
import { CreateProjectComponent } from './create-project/create-project.component';
import { PortfolioItemComponent } from './portfolio-item/portfolio-item.component';
import { ReviewComponent } from './review/review.component';
import { AuthGuard } from './guards/auth.guard';
import { SkillListComponent } from './components/skill/skill-list/skill-list.component';
import { FreelancerSkillsComponent } from './components/skill/freelancer-skills/freelancer-skills.component';
import { SkillCreateComponent } from './components/skill/skill-create/skill-create.component';
import { AddSkillsComponent } from './components/skill/add-skills/add-skills.component';
import { ReviewsForFreelancerComponent } from './components/review/reviews-for-freelancer/reviews-for-freelancer.component';
import { ReviewsForClientComponent } from './components/review/reviews-for-client/reviews-for-client.component';
import { ReviewCreateComponent } from './components/review/review-create/review-create.component';
import { ReviewUpdateComponent } from './components/review/review-update/review-update.component';
import { DeleteReviewComponent } from './components/review/delete-review/delete-review.component';
import { ManageSkillsComponent } from './components/requiredskill/manage-skills/manage-skills.component';
import { ProjectUpdateComponent } from './components/project/project-update/project-update.component';
import { ManageClientComponent } from './components/client/manage-client/manage-client.component';
import { LoginComponent } from './components/authentication/login/login.component';
import { RegisterComponent } from './components/authentication/register/register.component';
import { RegisterClientComponent } from './components/authentication/register-client/register-client.component';
import { RegisterFreelancerComponent } from './components/authentication/register-freelancer/register-freelancer.component';

export const routes: Routes = [
  { path: '', redirectTo: '/projects', pathMatch: 'full' },
  { path: 'projects', component: ProjectListComponent },
  { path: 'project/:id', component: ProjectDetailComponent },
  { path: 'create-project', component: CreateProjectComponent, canActivate: [AuthGuard] },
  { path: 'portfolio', component: PortfolioItemComponent },
  { path: 'reviews', component: ReviewComponent },
  

//skill
  {
    path: 'skills',
    component: SkillListComponent 
  },
  {
    path: 'skills/freelancer/:freelancerId',
    component: FreelancerSkillsComponent 
  },
  {
    path: 'skills/create',
    component: SkillCreateComponent 
  },
  {
    path: 'skills/freelancer/:freelancerId/add',
    component: AddSkillsComponent 
  },


  //review
  {
    path: 'review/freelancer/:freelancerId',
    component: ReviewsForFreelancerComponent 
  },
  {
    path: 'review/client/:clientId',
    component: ReviewsForClientComponent 
  },
  {
    path: 'review/client/:clientId/freelancer/:freelancerId',
    component: ReviewCreateComponent 
  },
  {
    path: 'review/update/:reviewId',
    component: ReviewUpdateComponent 
  },
  {
    path: 'review/delete-review/:reviewId',
    component: DeleteReviewComponent
  },
  

  //project
  
  { 
    path: 'project/:projectId/manage-skills', 
    component: ManageSkillsComponent 
  },
  { 
    path: 'project/project-list', 
    component: ProjectListComponent 
  }, 
  { 
    path: 'project/:projectId/update', 
    component: ProjectUpdateComponent 
  }, 
  { 
    path: 'project/:id', 
    component: ProjectDetailComponent 
  },
//client
  { 
    path: 'client/manage-client/:clientId', 
    component: ManageClientComponent 
  },



  //account
  { 
    path: 'account/login', 
    component: LoginComponent 
  },
  { 
    path: 'account/register', 
    component: RegisterComponent 
  },
  { 
    path: 'account/register/client', 
    component: RegisterClientComponent 
  },
  { 
    path: 'account/register/freelancer', 
    component: RegisterFreelancerComponent 
  },
];
