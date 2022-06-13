import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class StudentGroupService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getUserJoinGroup(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + 'StudentGroups/getAllStudentGroupsThatUserHasJoined');
  }

  getAllStudentGroups(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + 'StudentGroups/getAllStudentGroups');
  }

  getAllPendingRequestForUser(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + 'StudentGroups/GetPendingRequestForUser');
  }

  getAllStudentGroupForCreator(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + 'StudentGroups/getAllStudentGroupsForGroupCreator')
  }
  
  getStudentGroup(groupId: number): Observable<any> {
    return this.http.get<any>(this.baseUrl + 'StudentGroups/getStudentGroup/' + groupId);
  }

  createStudentGroup(group: any) {
    return this.http.post(this.baseUrl + 'StudentGroups/', group);
  }

  deletAttendance(groupId: number) {
    return this.http.delete(this.baseUrl + 'StudentGroups/' + groupId);
  }
}
