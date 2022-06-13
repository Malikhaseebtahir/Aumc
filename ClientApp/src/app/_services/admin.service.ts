import { Observable } from 'rxjs';
import { User } from '../_models/user';
import { Injectable } from '@angular/core';
import { Interest } from './../_models/Interest';
import { environment } from '../../environments/environment';
import { HttpClient } from '../../../node_modules/@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getInterest(): Observable<Interest[]> {
    return this.http.get<Interest[]>(this.baseUrl + 'Interests');
  }

  createInterest(interest: Interest): Observable<Interest> {
    return this.http.post<Interest>(this.baseUrl + 'Interests/', interest);
  }

  getUsersWithRoles() {
    return this.http.get(this.baseUrl + 'admin/userswithroles');
  }

  updateUserRoles(user: User, roles: {}) {
    return this.http.post(this.baseUrl + 'admin/editRoles/' + user.userName, roles);
  }

  getPhotosForApproval(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + 'admin/photosForModeration');
  }

  approvePhoto(photoId) {
    return this.http.post(this.baseUrl + 'admin/approvePhoto/' + photoId, {});
  }

  rejectPhoto(photoId) {
    return this.http.post(this.baseUrl + 'admin/rejectPhoto/' + photoId, {});
  }

  getGroupForApproval(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + 'admin/groupsForModeration/');
  }

  getStudentGroupsForApproval(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + 'admin/studentGroupsForModeration/')
  }

  approveGroup(groupId: number) {
    return this.http.post(this.baseUrl + 'admin/approveGroup/' + groupId, {});
  }

  approveStudentGroup(groupId: number) {
    return this.http.post(this.baseUrl + 'admin/approveStudentGroup/' + groupId, {});
  }

  rejectGroup(groupId: number) {
    return this.http.post(this.baseUrl + 'admin/rejectGroup/' + groupId, {});
  }

  rejectStudentGroup(groupId: number) {
    return this.http.post(this.baseUrl + 'admin/rejectStudentGroup/' + groupId, {});
  }

  getPostForApproval() {
    return this.http.get(this.baseUrl + 'admin/postForModeration/');
  }

  approvePost(postId: number) {
    return this.http.post(this.baseUrl + 'admin/approvePost/' + postId, {});
  }

  rejectPost(postId: number) {
    return this.http.post(this.baseUrl + 'admin/rejectPost/' + postId, {});
  }

  deleteUser(userId: number) {
    return this.http.delete(this.baseUrl + 'admin/DeleteUser/' + userId);
  }

  getInterestForApproval(): Observable<Interest[]> {
    return this.http.get<Interest[]>(this.baseUrl + 'admin/interestsForModeration');
  }

  approveInterest(interestId: number) {
    return this.http.post(this.baseUrl + 'admin/approveInterest/' + interestId, {});
  }

  getReport(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + 'admin/reportsForModeration');
  }

  approveReport(approveReport) {
    return this.http.put(this.baseUrl + 'admin/approveReport/', approveReport);
  }

  rejectReport(reportId: number) {
    return this.http.delete(this.baseUrl + 'admin/rejectReport/' + reportId);
  }

  getDisabledUser(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + 'admin/disabledUser');
  }

  enableUser(userId: number) {
    return this.http.put(this.baseUrl + 'admin/enableUser/' + userId, {});
  }

  getListOfBlockUser(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + 'admin/getAllTheBlockUserReports');
  }
}
