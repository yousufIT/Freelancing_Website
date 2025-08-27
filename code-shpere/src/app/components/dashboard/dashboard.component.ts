import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { DashboardService } from '../../services/dashboard.service';
import { SkillService } from '../../services/skill.service';
import { DashboardSummary } from '../../models/dashboard-summary.model';
import { Skill } from '../../models/skill';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  summary: DashboardSummary | null = null;
  loading = true;
  error: string | null = null;

  // Skills management
  skills: Skill[] = [];
  newSkillName = '';
  editingSkill: Skill | null = null;

  constructor(
    private dashboardService: DashboardService,
    private skillService: SkillService
  ) {}

  ngOnInit(): void {
    this.load();
    this.loadSkills();
  }

  // === Dashboard Summary ===
  load() {
    this.loading = true;
    this.dashboardService.getSummary().subscribe({
      next: (s) => { this.summary = s; this.loading = false; },
      error: (err) => {
        console.error(err);
        this.error = 'Failed to load dashboard';
        this.loading = false;
      }
    });
  }

  // === Skills Management ===
  loadSkills() {
    this.skillService.getAllSkills().subscribe({
      next: (skills) => this.skills = skills,
      error: (err) => console.error('Failed to load skills', err)
    });
  }

  addSkill() {
    if (!this.newSkillName.trim()) return;
    this.skillService.createSkill({ name: this.newSkillName }).subscribe({
      next: (s) => {
        this.skills.push(s);
        this.newSkillName = '';
      },
      error: (err) => console.error('Failed to add skill', err)
    });
  }

  startEdit(skill: Skill) {
    this.editingSkill = { ...skill }; // copy
  }

  cancelEdit() {
    this.editingSkill = null;
  }

  saveEdit() {
    if (!this.editingSkill) return;
    this.skillService.updateSkill(this.editingSkill.id, { name: this.editingSkill.name }).subscribe({
      next: (updated) => {
        const index = this.skills.findIndex(s => s.id === updated.id);
        if (index !== -1) this.skills[index] = updated;
        this.editingSkill = null;
      },
      error: (err) => console.error('Failed to update skill', err)
    });
  }

  deleteSkill(id: number) {
    if (!confirm('Are you sure you want to delete this skill?')) return;
    this.skillService.deleteSkill(id).subscribe({
      next: () => {
        this.skills = this.skills.filter(s => s.id !== id);
      },
      error: (err) => console.error('Failed to delete skill', err)
    });
  }
}
