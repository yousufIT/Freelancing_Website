// import { Injectable } from '@angular/core';
// import { HttpClient } from '@angular/common/http';
// import { catchError, Observable } from 'rxjs';

// @Injectable({
//   providedIn: 'root'
// })
// export class ProjectService {
//   private apiUrl = 'https://localhost:7240/api/projects';

//   constructor(private http: HttpClient) {}

//   getProjects(): Observable<any> {
//     return this.http.get<any>(`${this.apiUrl}`).pipe(
      
//     );
//   }

//   getProject(id: number): Observable<any> {
//     return this.http.get<any>(`${this.apiUrl}/${id}`);
//   }

//   createProject(project: any): Observable<any> {
//     return this.http.post<any>(this.apiUrl, project);
//   }
// }



import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Project } from '../models/project';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  private apiUrl = `${environment.apiUrl}/projects`;

  constructor(private http: HttpClient) { }

  getProjectById(id: number): Observable<Project> {
    return this.http.get<Project>(`${this.apiUrl}/${id}`);
  }

  createProject(project: Project): Observable<Project> {
    return this.http.post<Project>(this.apiUrl, project);
  }

  updateProject(project: Project): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${project.id}`, project);
  }

  deleteProject(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
