import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Skill } from '../models/skill';
import { environment } from '../../environments/environment';
import { SkillForCreate } from '../models/for-create/skill-for-create';

@Injectable({
  providedIn: 'root'
})
export class RequiredSkillService {

  private apiUrl = `${environment.apiUrl}/RequiredSkills`;

  constructor(private http: HttpClient) { }
  getSkillsForProject(projectId:number):Observable<Skill[]>{
    return this.http.get<Skill[]>(`${this.apiUrl}/project/${projectId}`);
  }

  addSkillsToProject(projectId:number,skillIds: number[]): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/project/${projectId}`, skillIds);
  }

  updateSkillsForProject(skillId:number,skill: SkillForCreate): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${skillId}`, skill);
  }

  removeSkillFromProject(projectId: number,skillId:number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/project/${projectId}/skill/${skillId}`);
  }
}
