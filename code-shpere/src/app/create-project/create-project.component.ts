import { Component } from '@angular/core';
import { ProjectService } from '../services/project.service';
import { ProjectForCreate } from '../models/for-create/project-for-create';
import { FormsModule } from '@angular/forms';

@Component({
  standalone: true,
  imports: [FormsModule],
  selector: 'app-create-project',
  templateUrl: './create-project.component.html',
})
export class CreateProjectComponent {
  project: ProjectForCreate = {
    title: '',
    description: '',
    budget: 0,
    status: ''
  };

  clientId: number = 1; // Assuming you have the client ID from somewhere

  constructor(private projectService: ProjectService) {}

  onSubmit() {
    // Call the createProject method when the form is submitted
    this.createProject();
  }

  createProject() {
    this.projectService.createProject(this.clientId, this.project).subscribe(
      (response) => {
        console.log('Project created successfully', response);
      },
      (error) => {
        console.error('Error creating project', error);
      }
    );
  }
}
