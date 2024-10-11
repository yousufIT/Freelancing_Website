import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Skill } from '../models/skill';
import { environment } from '../../environments/environment';
import { SkillForCreate } from '../models/for-create/skill-for-create';

@Injectable({
  providedIn: 'root'
})
export class SkillService {

  private apiUrl = `${environment.apiUrl}/Skills`;

  constructor(private http: HttpClient) { }

  getAllSkills(): Observable<Skill[]> {
    return this.http.get<Skill[]>(`${this.apiUrl}`);
  }
  getSkillsForFreelancer(freelancerId:number):Observable<Skill[]>{
    return this.http.get<Skill[]>(`${this.apiUrl}/Freelancer/${freelancerId}`);
  }
  getSkillById(id: number): Observable<Skill> {
    return this.http.get<Skill>(`${this.apiUrl}/${id}`);
  }

  createSkill(skill: SkillForCreate): Observable<Skill> {
    return this.http.post<Skill>(this.apiUrl, skill);
  }
  addSkillsForFreelancer(freelancerId:number,skillsIds:number[]):Observable<void>{
    return this.http.post<void>(`${this.apiUrl}/Freelancer/${freelancerId}`, skillsIds)
  }
  updateSkill(id:number,skill: SkillForCreate): Observable<Skill> {
    return this.http.put<Skill>(`${this.apiUrl}/${id}`, skill);
  }

  deleteSkill(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
