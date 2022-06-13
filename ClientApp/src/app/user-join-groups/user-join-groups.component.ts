import { StudentGroupService } from 'src/app/_services/student-group.service';
import { Component, OnInit } from '@angular/core';
import { AuthService } from './../_services/auth.service';
import { GroupService } from 'src/app/_services/group.service';

@Component({
  selector: 'app-user-join-groups',
  templateUrl: './user-join-groups.component.html',
  styleUrls: ['./user-join-groups.component.css']
})
export class UserJoinGroupsComponent implements OnInit {
  
  groups: any[] = [];

  constructor(
    private studentGroupService: StudentGroupService,
    private authService: AuthService,
    private groupService: GroupService) { }

  ngOnInit() {
    this.groupService.getGroupsUserHasJoin(this.authService.decodedToken.nameid)
      .subscribe(groups => this.groups = groups);
    }
    
  loadStudyGroups() {
    this.groupService.getGroupsUserHasJoin(this.authService.decodedToken.nameid)
      .subscribe(groups => this.groups = groups);
  }

  loadStudentGroups() {
    this.studentGroupService.getUserJoinGroup()
      .subscribe(groups => this.groups = groups);
  }

}
