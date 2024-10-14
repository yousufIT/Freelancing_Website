// project-details.component.ts
import { Component, OnInit, Pipe } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ProjectService } from 'src/app/services/project.service';
import { Project } from 'src/app/models/project';
import { CommonModule } from '@angular/common';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  standalone: true,
  imports: [CommonModule, RouterLink],
  selector: 'app-project-details',
  templateUrl: './project-details.component.html',
  styleUrls: ['./project-details.component.css']
})
export class ProjectDetailsComponent implements OnInit {
  project: Project | null = null; // Initialized to null
  errorMessage: string = ''; // To store error messages
  auth:AuthService;
  clientId:number=0;


  constructor(
    private projectService: ProjectService,
    private route: ActivatedRoute,
    private router: Router,
    private authService:AuthService
  ) {
    this.auth=authService;
  }

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
        this.clientId=project.clientId;
        console.log(this.clientId);
        
      },
      (error) => {
        this.errorMessage = 'Error fetching project details: ' + error.message;
      }
    );
  }

  goBack(): void {
    this.router.navigate(['/projects']); // Adjust the path as necessary
  }

  edit(): void {
    this.router.navigate(['/project/update',this.project?.id])
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
