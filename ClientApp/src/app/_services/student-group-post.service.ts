import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class StudentGroupPostService {

  baseUrl = environment.apiUrl + 'StudentGroupPosts/';

  constructor(private http: HttpClient) { }

  getAllStudentGroupPost(groupId: number): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + 'getAllApprovedStudentGroupPosts/' + groupId);
  }

  getAllStudentGroupPostsForApproval(groupId: number): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + 'getAllStudentGroupPostsForApproval/' + groupId);
  }

  approveStudentGroupPost(postId: number) {
    return this.http.post(this.baseUrl + 'approveStudentGroupPost/' + postId, {});
  }

  rejectStudentGroupPost(postId: number) {
    return this.http.delete(this.baseUrl + 'removeStudentGroupPost/' + postId);
  }

  createStudentGroupPost(post: any) {
    return this.http.post(this.baseUrl, post);
  }
}
