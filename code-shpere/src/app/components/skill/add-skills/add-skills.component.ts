import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, ActivatedRoute,RouterLink } from '@angular/router';
import { SkillService } from 'src/app/services/skill.service';

@Component({
  selector: 'app-add-skills',
  standalone: true,
  imports: [FormsModule, CommonModule, RouterLink],
  templateUrl: './add-skills.component.html',
  styleUrls: ['./add-skills.component.css']
})
export class AddSkillsComponent {
  freelancerId!: number;
  skillIds: number[] = []; 
  availableSkills: { id: number; name: string }[] = [];

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private skillService: SkillService
  ) {
    const id = this.route.snapshot.paramMap.get('freelancerId');
    this.freelancerId = id ? +id : 0; 
  }

  ngOnInit(): void {
    this.fetchSkills();
  }

  fetchSkills(): void {
    this.skillService.getAllSkills().subscribe(skills => {
      this.availableSkills = skills;

      // Fetch freelancer existing skills
      this.skillService.getSkillsForFreelancer(this.freelancerId).subscribe(fSkills => {
        this.skillIds = fSkills.map(s => s.id);
      });
    });
  }

  toggleSkill(skillId: number): void {
    const index = this.skillIds.indexOf(skillId);
    if (index > -1) {
      this.skillIds.splice(index, 1); // Remove if unselected
    } else {
      this.skillIds.push(skillId); // Add if selected
    }
  }

  isSelected(skillId: number): boolean {
    return this.skillIds.includes(skillId);
  }

  addSkills(): void {
    if (this.skillIds.length === 0) {
      alert('Please select at least one skill.');
      return;
    }

    // Call API to save skills
    this.skillService.addSkillsForFreelancer(this.freelancerId, this.skillIds).subscribe({
      next: () => {
        // ✅ Navigate to manage-freelancer component after saving
        this.router.navigate([`/freelancer/${this.freelancerId}`]);
      },
      error: (err) => {
        console.error(err);
        alert('Failed to save skills.');
      }
    });
  }
}
