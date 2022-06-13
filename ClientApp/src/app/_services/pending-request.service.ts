import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from './../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PendingRequestService {

  baseUrl = environment.apiUrl + 'PendingRequests/';

  constructor(private http: HttpClient) { }

  getPendingRequestForUser(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + 'GetPendingRequestForUser');
  }

  getPendingRequestForAdmin(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + 'GetPendingRequestForGroupAdmin')
  }

  approveRequest(attendanceDetails: any) {
    return this.http.post(this.baseUrl + 'approveAttendance', attendanceDetails);
  }

  rejectRequest(attendanceDetails: any) {
    return this.http.post(this.baseUrl + 'rejectAttendance', attendanceDetails);
  }

  cancelRequest(groupId: number) {
    return this.http.delete(this.baseUrl + groupId);
  }
}
