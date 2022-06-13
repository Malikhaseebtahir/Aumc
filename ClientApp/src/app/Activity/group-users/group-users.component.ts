import { AuthService } from './../../_services/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { GroupService } from './../../_services/group.service';
import { AlertifyService } from './../../_services/alertify.service';

@Component({
  selector: 'app-group-users',
  templateUrl: './group-users.component.html',
  styleUrls: ['./group-users.component.css']
})
export class GroupUsersComponent implements OnInit {
  
  groupId: number;
  groupUsers: any[] = [];

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    public authService: AuthService,
    private alertify: AlertifyService,
    private groupService: GroupService) { }

  ngOnInit() {
    this.groupId = +this.route.snapshot.paramMap.get('id');

    if (this.groupId) {
      this.groupService.getGroupUsers(this.groupId)
        .subscribe(groupUsers => this.groupUsers = groupUsers)
    }
  }

  leaveGroup(groupId: number, userId: number) {
    var user = this.groupUsers.find(gu => gu.user.id == userId);

    debugger;

    this.alertify.confirm('Are you sure you want to leave the group', () => {
      this.groupService.leaveGroup(groupId)
        .subscribe(() => {
          if (user.user.teacherOrStudent == 'Teacher') {
            this.router.navigate(['/teacher']);
          } else {
            this.router.navigate(['/student']);
          }
          this.alertify.success('you left the group')
        },
        () => this.alertify.error('something went wrong'));
    });
  }
}
