import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class StudentGroupAttendanceService {

  baseUrl = environment.apiUrl + 'StudentGroupAttendances/';

  constructor(private http: HttpClient) { }

  getPendingRequestForAdmin(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl);
  }

  joinStudentGroupAttendance(attendance: any) {
    return this.http.post(this.baseUrl, attendance);
  }

  approveStudentGroupAttendance(attendance: any) {
    return this.http.post(this.baseUrl + 'approveStudentGroupAttendance', attendance)
  }

  rejectStudentGroupAttendance(attendance: any) {
    return this.http.post(this.baseUrl + 'rejectStudentGroupAttendance', attendance);
  }

  getStudentGroupUsers(groupId: number): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + 'StudentGroupUsers/' + groupId);
  }

  leaveGroup(groupId) {
    return this.http.delete(this.baseUrl + 'leaveGroup/' + groupId);
  }
}
