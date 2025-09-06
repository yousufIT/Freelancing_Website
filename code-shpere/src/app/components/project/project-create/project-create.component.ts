import { Component, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
import { ProjectService } from 'src/app/services/project.service';
import { ProjectForCreate } from 'src/app/models/for-create/project-for-create';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Skill } from 'src/app/models/skill';
import { AuthService } from 'src/app/services/auth.service';
import { NumericRangeDirective } from 'src/app/directives/numeric-range.directive';

@Component({
  selector: 'app-project-create',
  standalone: true,
  templateUrl: './project-create.component.html',
  styleUrls: ['./project-create.component.css'],
  imports: [CommonModule, FormsModule, NumericRangeDirective]
})
export class ProjectCreateComponent {
  project: ProjectForCreate = { title: '', description: '', budget: 0, status: '',clientId:0  };
  @Output() projectCreated = new EventEmitter<void>();
  skills: Skill[] = [];
  selectedSkills: number[] = [];

  constructor(private projectService: ProjectService,
    private authService:AuthService,
     private router: Router) {}

  createProject(): void {
    this.project.clientId = this.authService.getUserId();
    this.projectService.createProject(this.authService.getUserId(),this.project).subscribe(() => {
      // Emit an event to notify the parent that the project was created
      this.projectCreated.emit();
      
      

      // Optionally, reset the form after submission
      this.project = { title: '', description: '', budget: 0, status: '',clientId:0 };
    });
  }


}
