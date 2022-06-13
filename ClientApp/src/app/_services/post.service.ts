import { Observable } from 'rxjs';
import { Post } from '../_models/Post';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { keyValuePairs } from './../_models/PostCategory';
import { environment } from './../../environments/environment.prod';

@Injectable({
  providedIn: 'root'
})
export class PostService {
  baseUrl = environment.apiUrl + 'posts/';

  constructor(private http: HttpClient) { }

  getPostCategories(): Observable<keyValuePairs> {
    return this.http.get<keyValuePairs>(this.baseUrl + 'getPostCategories/');
  }

  createPost(post: Post): Observable<Post> {
    return this.http.post<Post>(this.baseUrl, post);
  }

  updatePost(post: Post) {
    return this.http.put(this.baseUrl + post.id, post);
  }

  deletePost(postId: number): Observable<any> {
    return this.http.delete(this.baseUrl + postId);
  }
}