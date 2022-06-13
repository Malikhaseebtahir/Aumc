import { environment } from './../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StudentGroupCommentService {

  baseUrl = environment.apiUrl + 'StudentGroupComments/';

  constructor(private http: HttpClient) { }

  getComments(groupId: number): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + 'allStudentGroupCommentsByGroup/' + groupId);
  }

  addComment(comment: any) {
    return this.http.post(this.baseUrl + 'AddStudentGroupComment/', comment);
  }

  deleteComment(commentId: number) {
    return this.http.delete(this.baseUrl + 'deleteStudentGroupComment/' + commentId);
  }
}
