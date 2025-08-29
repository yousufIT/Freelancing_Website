import { Component, OnInit } from '@angular/core';
import { ProjectService } from 'src/app/services/project.service';
import { Project } from 'src/app/models/project';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { Skill } from 'src/app/models/skill';
import { SkillService } from 'src/app/services/skill.service';
import { PaginationMetaData } from 'src/app/models/data-with-pagination';
import { CommonModule } from '@angular/common';
import { ProjectCreateComponent } from '../project-create/project-create.component';
import { AuthService } from 'src/app/services/auth.service';
import { SignalRService } from 'src/app/services/signalr.service';

@Component({
  selector: 'app-project-list',
  standalone: true,
  templateUrl: './project-list.component.html',
  styleUrls: ['./project-list.component.css'],
  imports: [FormsModule, CommonModule, ProjectCreateComponent]
})
export class ProjectListComponent implements OnInit {
  projects: Project[] = [];
  open: boolean = false;
  skillIds: string = ''; 
  skills: Skill[] = [];
  skillsIds: number[] = [];
  bidCounts:number=0;
  paginationMetaData: PaginationMetaData = {
    currentPage: 1,
    pageSize: 10,
    totalItemCount: 0,
    totalPageCount: 0
  };
  currentPage: number = 1;
  pageSize: number = 10;
  totalPageCount: number = 0;
  showCreateProject = false;
  auth: AuthService;

  constructor(private projectService: ProjectService,private skillService:SkillService, private authService: AuthService, private router: Router) {
    this.auth = this.authService;
  }

  ngOnInit(): void {
    this.loadFilteredProjects();
    this.fetchAllSkills();
    
    
  }

  loadFilteredProjects(): void {
      this.projectService.getProjectsFilteredBySkills(this.skillIds, this.currentPage, this.pageSize)
        .subscribe((data) => {
          this.projects = data.items;
          this.paginationMetaData = data.paginationMetaData;
          this.totalPageCount=this.paginationMetaData.totalPageCount;
          this.projects.forEach(project => {
          project.bidCount = project.bids.length;
          });
         
          
        });
  }

  viewProject(id: number): void {
    this.router.navigate(['/project', id]);
  }

  onSkillIdsChange(newSkillIds: string): void {
    this.skillIds = newSkillIds;
    this.loadFilteredProjects(); // Reload projects when skill IDs change
  }

  fetchAllSkills(): void {
    this.skillService.getAllSkills().subscribe({
      next: (data) => {
        this.skills = data;
      },
      error: (error) => {
        console.error('Error fetching skills:', error);
      }
    });
  }

  onSkillChange(skillId: number, event: Event): void {
    const target = event.target as HTMLInputElement;
    const isChecked = target.checked; 
  
    if (isChecked) {
      this.skillsIds.push(skillId);
      this.skillIds = JSON.stringify(this.skillsIds).replaceAll("[","").replaceAll("]","");
      this.loadFilteredProjects();
    } else {
      this.skillsIds.splice(this.skillsIds.indexOf(skillId),1);
      this.skillIds = JSON.stringify(this.skillsIds).replaceAll("[","").replaceAll("]","");
      this.loadFilteredProjects();
    }
  }

  toggleSkills(){
    this.open = !this.open;
  }

  toggleCreateProject(): void {
    this.showCreateProject = !this.showCreateProject;
  }

  onProjectCreated(): void {
    // Hide the create project form
    this.showCreateProject = false;

    // Reload the project list to include the new project
    this.loadFilteredProjects();
  }
  goToPage(page: number): void {
    this.currentPage = page;
    this.loadFilteredProjects();
  }
}
