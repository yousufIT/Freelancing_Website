import { Component, OnInit, Pipe } from '@angular/core';
import { ProjectService } from '../services/project.service';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

@Component({
  standalone: true,
  selector: 'app-project-list',
  templateUrl: './project-list.component.html',
  imports: [FormsModule, RouterLink, CommonModule, HttpClientModule]
})
export class ProjectListComponent implements OnInit {
  projects: any[] = [];

  constructor(private projectService: ProjectService) {}

  ngOnInit(): void {
    this.projectService.getProjects().subscribe((data) => {
      this.projects = data;
    });
  }
}
