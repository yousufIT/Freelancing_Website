import { Component } from '@angular/core';
import { ProjectService } from '../services/project.service';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

@Component({
  standalone: true,
  selector: 'app-create-project',
  templateUrl: './create-project.component.html',
  imports: [FormsModule,CommonModule, HttpClientModule]
})
export class CreateProjectComponent {
  project = {
    title: '',
    description: '',
    budget: 0
  };

  constructor(private projectService: ProjectService, private router: Router) {}

  createProject() {
    this.projectService.createProject(this.project).subscribe(() => {
      this.router.navigate(['/projects']);
    });
  }
}
