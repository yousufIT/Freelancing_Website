import { Component, OnInit } from '@angular/core';
import { ProjectService } from 'src/app/services/project.service';
import { Project } from 'src/app/models/project';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-project-list',
  standalone: true,
  templateUrl: './project-list.component.html',
  styleUrls: ['./project-list.component.css'],
  imports: [FormsModule]
})
export class ProjectListComponent implements OnInit {
  projects: Project[] = [];
  skillIds: string = ''; // The skill IDs to filter by
  pageNumber: number = 1; // Current page number
  pageSize: number = 10;  // Number of projects per page

  constructor(private projectService: ProjectService, private router: Router) {}

  ngOnInit(): void {
    this.loadFilteredProjects();
  }

  loadFilteredProjects(): void {
    if (this.skillIds) {
      this.projectService.getProjectsFilteredBySkills(this.skillIds, this.pageNumber, this.pageSize)
        .subscribe((data) => {
          this.projects = data;
        });
    }
  }

  viewProject(id: number): void {
    this.router.navigate(['/projects', id]);
  }

  onSkillIdsChange(newSkillIds: string): void {
    this.skillIds = newSkillIds;
    this.loadFilteredProjects(); // Reload projects when skill IDs change
  }
}
