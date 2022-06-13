import { AuthService } from './../../_services/auth.service';
import { AlertifyService } from './../../_services/alertify.service';
import { ActivatedRoute, Router } from '@angular/router';
import { StudentGroupAttendanceService } from 'src/app/_services/student-group-attendance.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-student-group-users',
  templateUrl: './student-group-users.component.html',
  styleUrls: ['./student-group-users.component.css']
})
export class StudentGroupUsersComponent implements OnInit {
  
  groupId: number;
  groupUsers: any[] = [];

  constructor(
    private router: Router,
    public authService: AuthService,
    private route: ActivatedRoute,
    private alertify: AlertifyService,
    private studentGroupAttendanceService: StudentGroupAttendanceService) { }

  ngOnInit() {
    this.groupId = +this.route.snapshot.paramMap.get('id');

    if (this.groupId) {
      this.studentGroupAttendanceService.getStudentGroupUsers(this.groupId)
        .subscribe(groupUsers => {
          this.groupUsers = groupUsers;
          console.log(groupUsers);
        })
    }
  }

  leaveGroup(groupId: number, userId: number) {
    var user = this.groupUsers.find(gu => gu.user.id == userId);

    this.alertify.confirm('Are you sure you want to leave the group', () => {
      this.studentGroupAttendanceService.leaveGroup(groupId)
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
