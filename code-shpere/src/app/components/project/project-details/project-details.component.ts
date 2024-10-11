// project-details.component.ts
import { Component, OnInit, Pipe } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProjectService } from 'src/app/services/project.service';
import { Project } from 'src/app/models/project';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  imports: [CommonModule],
  selector: 'app-project-details',
  templateUrl: './project-details.component.html',
  styleUrls: ['./project-details.component.css']
})
export class ProjectDetailsComponent implements OnInit {
  project: Project | null = null; // Initialized to null
  errorMessage: string = ''; // To store error messages

  constructor(
    private projectService: ProjectService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    const projectId = Number(this.route.snapshot.paramMap.get('id')); // Assuming you're using route parameters
    if (projectId) {
      this.getProjectDetails(projectId);
    } else {
      this.errorMessage = 'Project ID is missing';
    }
  }

  getProjectDetails(id: number): void {
    this.projectService.getProjectById(id).subscribe(
      (project) => {
        this.project = project;
      },
      (error) => {
        this.errorMessage = 'Error fetching project details: ' + error.message;
      }
    );
  }

  goBack(): void {
    this.router.navigate(['/projects']); // Adjust the path as necessary
  }

  deleteProject(): void {
    if (this.project) {
      this.projectService.deleteProject(this.project.id).subscribe(
        () => {
          this.router.navigate(['/projects']); // Redirect after deletion
        },
        (error) => {
          this.errorMessage = 'Error deleting project: ' + error.message;
        }
      );
    }
  }
}
