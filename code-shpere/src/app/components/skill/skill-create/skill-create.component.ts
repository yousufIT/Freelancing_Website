import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { Subscription } from 'rxjs';
import { SkillForCreate } from 'src/app/models/for-create/skill-for-create';
import { SkillService } from 'src/app/services/skill.service';

@Component({
  selector: 'app-skill-create',
  standalone: true,
  imports: [FormsModule,CommonModule,RouterLink],
  templateUrl: './skill-create.component.html',
  styleUrls: ['./skill-create.component.css','../../../../assets/css/local-design.css']
})
export class SkillCreateComponent {
  skill: SkillForCreate = { name: '' }; 
  private subscription!: Subscription;

  constructor(private skillService: SkillService, private router: Router) {}

  ngOnInit(): void {}

  createSkill(): void {
    this.subscription = this.skillService.createSkill(this.skill).subscribe({
      next: (data) => {
        console.log('Skill created successfully:', data);
        this.router.navigate(['/Skills']);
      },
      error: (error) => {
        console.error('Error creating skill:', error);
      },
      complete: () => {
        console.log('Skill creation completed.');
      }
    });
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
}
