import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProjectService } from '../services/project.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Project } from '../models/project';

@Component({
  standalone: true,
  selector: 'app-project-detail',
  templateUrl: './project-detail.component.html',
  imports: [FormsModule,CommonModule, HttpClientModule]
})
export class ProjectDetailComponent implements OnInit {
  project!: Project;

  constructor(
    private route: ActivatedRoute,
    private projectService: ProjectService
  ) {}

  ngOnInit(): void {
    let id = Number(this.route.snapshot.paramMap.get('id'));
    if(isNaN(id)) id = 0;
    this.projectService.getProjectById(id).subscribe((data) => {
      this.project = data;
    });
  }
}
