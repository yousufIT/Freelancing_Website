import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProjectService } from 'src/app/services/project.service';
import { ProjectForCreate } from 'src/app/models/for-create/project-for-create';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-project-update',
  standalone: true,
  templateUrl: './project-update.component.html',
  styleUrls: ['./project-update.component.css'],
  imports: [CommonModule, FormsModule]
})
export class ProjectUpdateComponent implements OnInit {
  project: ProjectForCreate = {  title: '', description: '', budget: 0, status: '' };
  projectId: number = 0;

  constructor(
    private route: ActivatedRoute,
    private projectService: ProjectService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.projectId = this.route.snapshot.params['id'];
    this.loadProject(this.projectId);
  }

  loadProject(id: number): void {
    this.projectService.getProjectById(id).subscribe((data) => {
      this.project = data;
    });
  }

  updateProject(): void {
    this.projectService.updateProject(this.projectId, this.project).subscribe(() => {
      this.router.navigate(['/projects']);
    });
  }
}
