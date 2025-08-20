import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { Subscription } from 'rxjs';
import { Skill } from 'src/app/models/skill';
import { SkillService } from 'src/app/services/skill.service';

@Component({
  selector: 'app-freelancer-skills',
  standalone: true,
  imports: [FormsModule,CommonModule,RouterLink],
  templateUrl: './freelancer-skills.component.html',
  styleUrls: ['./freelancer-skills.component.css']
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
        this.router.navigate(['/'])
      },
      complete: () => {
        console.log('Fetching freelancer skills completed.');
      }
    });
  }
 
  goToAddRoute():void{
    this.router.navigate(['/skills/freelancer/add/',this.freelancerId]);
  }
  
}
