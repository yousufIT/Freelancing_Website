import { Component, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
import { ProjectService } from 'src/app/services/project.service';
import { ProjectForCreate } from 'src/app/models/for-create/project-for-create';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Skill } from 'src/app/models/skill';

@Component({
  selector: 'app-project-create',
  standalone: true,
  templateUrl: './project-create.component.html',
  styleUrls: ['./project-create.component.css'],
  imports: [CommonModule, FormsModule]
})
export class ProjectCreateComponent {
  project: ProjectForCreate = { title: '', description: '', budget: 0, status: '' };
  @Output() projectCreated = new EventEmitter<void>();
  skills: Skill[] = [];
  selectedSkills: number[] = [];

  constructor(private projectService: ProjectService, private router: Router) {}

  createProject(): void {
    this.projectService.createProject(1,this.project).subscribe(() => {
      // Emit an event to notify the parent that the project was created
      this.projectCreated.emit();

      // Optionally, reset the form after submission
      this.project = { title: '', description: '', budget: 0, status: '' };
    });
  }


}
