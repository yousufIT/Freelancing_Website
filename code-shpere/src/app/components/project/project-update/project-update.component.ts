import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProjectService } from 'src/app/services/project.service';
import { ProjectForCreate } from 'src/app/models/for-create/project-for-create';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ManageSkillsComponent } from '../../requiredskill/manage-skills/manage-skills.component';
import { Unsubscribable } from 'rxjs';
import { NumericRangeDirective } from 'src/app/directives/numeric-range.directive';

@Component({
  selector: 'app-project-update',
  standalone: true,
  templateUrl: './project-update.component.html',
  styleUrls: ['./project-update.component.css'],
  imports: [CommonModule, FormsModule, ManageSkillsComponent, NumericRangeDirective]
})
export class ProjectUpdateComponent implements OnInit {
  project: ProjectForCreate = { title: '', description: '', budget: 0, status: '',clientId:0 };
  projectId: number | undefined;

  constructor(
    private route: ActivatedRoute,
    private projectService: ProjectService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.projectId = +this.route.snapshot.paramMap.get('id')!; 
    if (this.projectId) {
      this.loadProject(this.projectId);
    } else {
      console.error("Project ID is missing.");
    }
  }
  

  loadProject(id: number): void {
    this.projectService.getProjectById(id).subscribe({
      next: (data) => {
        this.project = data;
      },
      error: (error) => {
        console.error('Error loading project:', error);
      }
    });
  }
  

  updateProject(): void {
    if (this.projectId && this.project) {
      this.projectService.updateProject(this.projectId, this.project).subscribe({
        next: () => {
          this.router.navigate(['/projects']); // Navigate to the projects page after update
        },
        error: (err) => {
          console.error("Error updating the project:", err);
        }
      });
    } else {
      console.error("Project data or ID is missing.");
    }
  }
  
}
