import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class StudyGroupPostCommentService {

  baseUrl = environment.apiUrl + 'PostComments/';

  constructor(private http: HttpClient) { }

  getPostComments(postId: number): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + 'allPostComments/' + postId);
  }

  addPostComment(comment: any) {
    return this.http.post(this.baseUrl + 'addPostComment', comment);
  }

  deletePostComment(commentId: number) {
    return this.http.delete(this.baseUrl + 'deletePostComment/' + commentId);
  }
}
