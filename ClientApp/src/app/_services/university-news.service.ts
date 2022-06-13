import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UniversityNewsService {

  baseUrl = environment.apiUrl + 'UniversityNews/';

  constructor(private http: HttpClient) { }

  getNews(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl);
  }

  addNews(news: any) {
    return this.http.post(this.baseUrl, news);
  }

  updateNews(news: any) {
    return this.http.put(this.baseUrl, news);
  }

  deleteNews(newsId: number) {
    return this.http.delete(this.baseUrl + newsId);
  }
}
