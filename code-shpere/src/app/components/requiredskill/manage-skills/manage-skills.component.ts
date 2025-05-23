import { CommonModule } from '@angular/common';
import { Component, OnInit, Input } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SkillForCreate } from 'src/app/models/for-create/skill-for-create';
import { Skill } from 'src/app/models/skill';
import { RequiredSkillService } from 'src/app/services/required-skill.service';
import { SkillService } from 'src/app/services/skill.service';


@Component({
  selector: 'app-manage-skills',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './manage-skills.component.html',
  styleUrl: './manage-skills.component.css'
})
export class ManageSkillsComponent implements OnInit {
  @Input() projectId!: number;
  skills: Skill[] = [];  
  skillIds: number[] = []; 
  allskills:Skill[]=[];
  checkBoxes: HTMLInputElement[] = [];

  constructor(
    private route: ActivatedRoute,
    private requiredSkillService: RequiredSkillService,
    private skillService:SkillService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.projectId = +this.route.snapshot.paramMap.get('id')!; 
    this.fetchSkills();
    this.fetchAllSkills();
  }
  fetchAllSkills(): void {
    this.skillService.getAllSkills().subscribe({
      next: (data) => {
        this.allskills = data;
      },
      error: (error) => {
        console.error('Error fetching skills:', error);
      }
    });
  }
  fetchSkills(): void {
    this.requiredSkillService.getSkillsForProject(this.projectId).subscribe({
      next: (data) => {
        this.skills = data;
      },
      error: (error) => {
        console.error('Error fetching skills:', error);
      }
    });
  }

  addSkills(): void {
    
    this.requiredSkillService.addSkillsToProject(this.projectId, this.skillIds).subscribe({
      next: () => {
        this.fetchSkills(); 
        this.skillIds = []; 
        this.checkBoxes.map(el => el.checked = false);
        this.checkBoxes = [];
      },
      error: (error) => {
        console.error('Error adding skills:', error);
      }
    });
  }

  removeSkill(skillId: number): void {
    this.requiredSkillService.removeSkillFromProject(this.projectId, skillId).subscribe({
      next: () => {
        this.fetchSkills(); 
      },
      error: (error) => {
        console.error('Error removing skill:', error);
      }
    });
  }

  onSkillChange(skillId: number, event: Event): void {
    const target = event.target as HTMLInputElement;
    const isChecked = target.checked; 
  
    if (isChecked) {
      this.skillIds.push(skillId);
      this.checkBoxes.push(target);
    } else {
      this.skillIds = this.skillIds.filter(id => id !== skillId);
      this.checkBoxes = this.checkBoxes.filter(el => el !== target);
    }
  }
  
}