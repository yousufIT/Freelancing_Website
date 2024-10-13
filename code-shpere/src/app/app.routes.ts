import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProjectListComponent } from './components/project/project-list/project-list.component';
import { ProjectDetailsComponent } from './components/project/project-details/project-details.component';
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
import { LogoutComponent } from './components/authentication/logout/logout.component';
import { BidCreateComponent } from './components/bid/bid-create/bid-create.component';
import { BidListComponent } from './components/bid/bid-list/bid-list.component';
import { BidUpdateComponent } from './components/bid/bid-update/bid-update.component';
import { ManageFreelancerComponent } from './components/freelancer/manage-freelancer/manage-freelancer.component';
import { PortfolioListComponent } from './components/profile/portfolio-list/portfolio-list.component';
import { PortfolioCreateComponent } from './components/profile/portfolio-create/portfolio-create.component';
import { AuthRoleGuard } from './guards/auth-role.guard';
import { FreelancerDetailsComponent } from './components/freelancer/freelancer-details/freelancer-details.component';
import { ClientDetailsComponent } from './components/client/client-details/client-details.component';
import { ProfileEditForClientGuard } from './guards/profile-edit-for-client.guard';
import { ProfileEditForFreelancerGuard } from './guards/profile-edit-for-freelancer.guard';
import { ClientUpdateComponent } from './components/client/client-update/client-update.component';
import { FreelancerUpdateComponent } from './components/freelancer/freelancer-update/freelancer-update.component';

export const routes: Routes = [
  { path: '', redirectTo: '/projects', pathMatch: 'full' },
  { path: 'projects', component: ProjectListComponent },
  { path: 'project/:id', component: ProjectDetailsComponent },
  { path: 'projects/:projectId/bids/create', component: BidCreateComponent },
  { path: 'projects/:projectId/bids', component: BidListComponent },
  { path: 'bids/:bidId/update', component: BidUpdateComponent },
  { path: 'profiles/:profileId/portfolio', component: PortfolioListComponent },
  { path: 'portfolio/:profileId/create', component: PortfolioCreateComponent },
  { path: 'profiles/:profileId/portfolio/:portfolioItemId/edit', component: PortfolioCreateComponent },

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
    path: 'skills/freelancer/add/:freelancerId',
    component: AddSkillsComponent ,
    canActivate: [AuthRoleGuard],
    data: {
      roles: ['Freelancer']
    }
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
    component: ReviewCreateComponent ,
    canActivate: [AuthRoleGuard],
    data: {
      roles: ['Client']
    }
  },
  {
    path: 'review/update/:reviewId',
    component: ReviewUpdateComponent ,
    canActivate: [AuthRoleGuard],
    data: {
      roles: ['Client']
    }
  },
  {
    path: 'review/delete-review/:reviewId',
    component: DeleteReviewComponent,
    canActivate: [AuthRoleGuard],
    data: {
      roles: ['Client']
    }
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
    path: 'project/update/:projectId', 
    component: ProjectUpdateComponent 
  }, 
  
//client
  { 
    path: 'client/:Id', 
    component: ManageClientComponent ,
    canActivate:[ProfileEditForClientGuard]
  },
  { 
    path: 'client/update/:Id', 
    component: ClientUpdateComponent ,
    canActivate:[ProfileEditForClientGuard]
  },
  { 
    path: 'client-details/:Id', 
    component: ClientDetailsComponent 
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
  { 
    path: 'account/logout', 
    component: LogoutComponent 
  },



  //freelancer
  { 
    path: 'freelancer/:Id', 
    component: ManageFreelancerComponent ,
    canActivate:[ProfileEditForFreelancerGuard]
  },
  { 
    path: 'freelancer-details/:Id', 
    component: FreelancerDetailsComponent 
  },
  { 
    path: 'freelancer/update/:Id', 
    component: FreelancerUpdateComponent ,
    canActivate:[ProfileEditForFreelancerGuard]
  },
  
];
