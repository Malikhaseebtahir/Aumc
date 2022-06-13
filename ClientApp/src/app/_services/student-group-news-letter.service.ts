import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class StudentGroupNewsLetterService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getAllStudentGroupNews(groupId: number): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + 'StudentGroupNewsLetters/getAllStudentGroupNews/' + groupId);
  }

  createNews(news: any) {
    return this.http.post(this.baseUrl + 'StudentGroupNewsLetters/', news);
  }

  updateNews(updatedNews) {
    return this.http.put(this.baseUrl + 'StudentGroupNewsLetters/updateStudentGroupNews', updatedNews);
  }

  deleteStudentGroupNews(newsId: number) {
    return this.http.delete(this.baseUrl + 'StudentGroupNewsLetters/' + newsId);
  }
}
