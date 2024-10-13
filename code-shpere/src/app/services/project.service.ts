import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Project } from '../models/project';
import { environment } from '../../environments/environment';
import { ProjectForCreate } from '../models/for-create/project-for-create';
import { DataWithPagination } from '../models/data-with-pagination';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  private apiUrl = `${environment.apiUrl}/Projects`;

  constructor(private http: HttpClient) { }
  getProjectsFilteredBySkills(Ids: string, pageNumber: number, pageSize: number):Observable<DataWithPagination<Project>>{
    if(Ids == '') Ids = '0';
    return this.http.get<DataWithPagination<Project>>(`${this.apiUrl}/Skill/${Ids}?pageNumber=${pageNumber}&pageSize=${pageSize}`);
  }
  getProjectById(id: number): Observable<Project> {
    return this.http.get<Project>(`${this.apiUrl}/${id}`);
  }

  createProject(clientId:number,project: ProjectForCreate): Observable<Project> {
    return this.http.post<Project>(`${this.apiUrl}/CLient/${clientId}`, project);
  }

  updateProject(id:number,project: ProjectForCreate): Observable<Project> {
    return this.http.put<Project>(`${this.apiUrl}/${id}`, project);
  }

  deleteProject(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
