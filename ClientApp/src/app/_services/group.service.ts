import { keyValuePairs } from './../_models/PostCategory';
import { Observable } from 'rxjs';
import { Group } from '../_models/Group';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SaveGroup } from 'src/app/_models/SaveGroup';
import { environment } from './../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GroupService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  joinGroup(group) {
    return this.http.post(this.baseUrl + 'GroupAttendance', group);
  }

  getGroupTypes(): Observable<keyValuePairs> {
    return this.http.get<keyValuePairs>(this.baseUrl + 'groupTypes/')
  }

  getGroups(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + 'groups/');
  }

  getGroupsByUser(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + 'groups/GroupsByUser');
  }

  getGroupsUserHasJoin(userId: number): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + 'groups/GetJoinGroups/' + userId);
  }

  getGroup(id: number): Observable<Group> {
    return this.http.get<Group>(this.baseUrl + 'groups/' + id);
  }

  createGroup(group: SaveGroup): Observable<SaveGroup> {
    return this.http.post<SaveGroup>(this.baseUrl + 'groups/', group);
  }
  
  deleteGroup(groupId: number) {
    return this.http.delete(this.baseUrl + 'groups/' + groupId);
  }

  addComment(comment) {
    return this.http.post(this.baseUrl + 'comments/AddComment', comment);
  }

  getComments(groupId: number): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + 'comments/allCommentsByGrouop/' + groupId);
  }

  getGroupUsers(groupId: number): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + 'GroupAttendance/GroupUsers/' + groupId);
  }

  leaveGroup(groupId: number) {
    return this.http.delete(this.baseUrl + 'GroupAttendance/leaveGroup/' + groupId);
  }

  deleteComment(commentId: number) {
    return this.http.delete(this.baseUrl + 'comments/deleteComment/' + commentId);
  }
}
