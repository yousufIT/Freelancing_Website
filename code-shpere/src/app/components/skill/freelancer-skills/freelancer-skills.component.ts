import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Skill } from 'src/app/models/skill';
import { SkillService } from 'src/app/services/skill.service';

@Component({
  selector: 'app-freelancer-skills',
  standalone: true,
  imports: [],
  templateUrl: './freelancer-skills.component.html',
  styleUrl: './freelancer-skills.component.css'
})
export class FreelancerSkillsComponent implements OnInit, OnDestroy{

  skills: Skill[] = [];
  private subscription!: Subscription;
  freelancerId!: number;
  
  constructor(private route: ActivatedRoute,
    private router: Router,
    private skillService: SkillService){
    let id = route.snapshot.paramMap.get('freelancerId');
    this.freelancerId=id?+id:0;

  }
  ngOnInit(): void {
    this.getSkillsForFreelancer(this.freelancerId);
  }
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  getSkillsForFreelancer(freelancerId: number): void {
    this.subscription = this.skillService.getSkillsForFreelancer(freelancerId).subscribe({
      next: (data) => {
        this.skills = data;
        console.log('Fetched skills for freelancer:', data);
      },
      error: (error) => {
        console.error('Error fetching skills for freelancer:', error);
      },
      complete: () => {
        console.log('Fetching freelancer skills completed.');
      }
    });
  }
 

}
