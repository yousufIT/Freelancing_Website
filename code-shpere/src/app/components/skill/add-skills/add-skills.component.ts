import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { SkillService } from 'src/app/services/skill.service';

@Component({
  selector: 'app-add-skills',
  standalone: true,
  imports: [FormsModule,CommonModule,RouterLink],
  templateUrl: './add-skills.component.html',
  styleUrls: ['./add-skills.component.css','../../../../assets/css/local-design.css']
})
export class AddSkillsComponent {
  freelancerId!: number;
  skillIds: number[] = []; 
  availableSkills: { id: number; name: string }[] = [];
  constructor(private route: ActivatedRoute, private skillService: SkillService) {
    const id = this.route.snapshot.paramMap.get('freelancerId');
    this.freelancerId = id ? +id : 0; 
  }

  ngOnInit(): void {
    this.fetchAvailableSkills();
}

  addSkills(): void {
    this.skillService.addSkillsForFreelancer(this.freelancerId, this.skillIds).subscribe({
      next: () => {
        console.log('add is done');
      },
      error: (error) => {
        console.error('the was problem:', error);
      }
    });
  }



fetchAvailableSkills(): void {
  this.skillService.getAllSkills().subscribe({
    next: (skills) => {
      this.availableSkills = skills;
      console.log(skills);
      
    },
    error: (error) => {
      console.error('there was problem :(', error);
    }
  });
}
onSkillChange(skillId: number, event: Event): void {
  const target = event.target as HTMLInputElement;
  const isChecked = target.checked; 

  if (isChecked) {
    this.skillIds.push(skillId)
  } else {
    
  }
}
}
