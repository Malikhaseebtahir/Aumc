import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class StudentGroupPostCommentService {

  baseUrl = environment.apiUrl + 'StudentGroupPostComments/';

  constructor(private http: HttpClient) { }

  getPostComments(postId: number): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + 'allPostComments/' + postId);
  }

  addPostComment(comment: any) {
    return this.http.post(this.baseUrl + 'addPostComment/', comment);
  }

  deletePostComment(postId: number) {
    return this.http.delete(this.baseUrl + 'deletePostComment/' + postId);
  }
}
