import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Skill } from 'src/app/models/skill';
import { RequiredSkillService } from 'src/app/services/required-skill.service';
import { SkillService } from 'src/app/services/skill.service';

@Component({
  selector: 'app-skills-for-project',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './skills-for-project.component.html',
  styleUrl: './skills-for-project.component.css'
})
export class SkillsForProjectComponent implements OnInit {
  projectId!: number;
  skills: Skill[] = [];  // Array to hold the skills

  constructor(
    private route: ActivatedRoute,
    private skillService: RequiredSkillService // Inject the SkillService
  ) {}

  ngOnInit(): void {
    this.projectId = +this.route.snapshot.paramMap.get('projectId')!; // Get the project ID from the route
    this.fetchSkillsForProject();
  }

  fetchSkillsForProject(): void {
    this.skillService.getSkillsForProject(this.projectId).subscribe({
      next: (data: Skill[]) => {
        this.skills = data;  // Assign fetched skills to the local array
      },
      error: (error) => {
        console.error('Error fetching skills:', error);
      }
    });
  }
}
