import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FollowingService {

  baseUrl = environment.apiUrl + 'Followings/';

  constructor(private http: HttpClient) { }

  getAllFollowees(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + 'GetAllFollowees')
  }

  getAllFollowers(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + 'GetAllFollowers');
  }

  followUser(followeeId: number) {
    return this.http.post(this.baseUrl + followeeId, {});
  }

  unFollowUser(userId: number) {
    return this.http.delete(this.baseUrl + userId);
  }

}
