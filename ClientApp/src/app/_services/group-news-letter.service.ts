import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class GroupNewsLetterService {

  baseUrl = environment.apiUrl + 'GroupNewsLetters/';

  constructor(private http: HttpClient) { }

  getGroupNews(groupId: number): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + 'getAllGroupNews/' + groupId); 
  }

  createGroupNews(news: any) {
    return this.http.post(this.baseUrl, news);
  }

  updateGroupNews(news: any) {
    return this.http.put(this.baseUrl + 'updateGroupNews/', news);
  }

  deleteGroupNews(newsId: number) {
    return this.http.delete(this.baseUrl + newsId);
  }
}
