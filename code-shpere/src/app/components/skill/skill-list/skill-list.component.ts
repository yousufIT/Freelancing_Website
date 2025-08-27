import { Component, OnInit, OnDestroy } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subscription } from 'rxjs';
import { SkillService } from '../../../services/skill.service';
import { Skill } from 'src/app/models/skill';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
@Component({
  selector: 'app-skill-list',
  standalone: true,
  imports:[CommonModule,RouterLink],
  templateUrl: './skill-list.component.html',
  styleUrls: ['./skill-list.component.css','../../../../assets/css/local-design.css'],
  providers: [SkillService] 
})
export class SkillListComponent implements OnInit, OnDestroy {

  skills: Skill[] = [];
  private subscription!: Subscription;

  constructor(private skillService: SkillService) { } 

  ngOnInit(): void {
    console.log('this is init')
    this.getAllSkills();
  }

  getAllSkills(): void {
    this.subscription = this.skillService.getAllSkills().subscribe({
      next: (data) => {
        this.skills = data;
        console.log(data);
      },
      error: (error) => {
        console.error('Error fetching skills:', error);
      },
      complete: () => {
        console.log('Fetching skills completed.');
      }
    });
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
}
//
//Done
//