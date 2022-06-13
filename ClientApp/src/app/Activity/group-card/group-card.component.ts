import { StudentGroupAttendanceService } from './../../_services/student-group-attendance.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { AuthService } from './../../_services/auth.service';
import { Component, Input, TemplateRef } from '@angular/core';
import { GroupService } from './../../_services/group.service';
import { AlertifyService } from './../../_services/alertify.service';

@Component({
  selector: 'app-group-card',
  templateUrl: './group-card.component.html',
  styleUrls: ['./group-card.component.css']
})
export class GroupCardComponent {

  @Input('group') group;
  
  bsModalRef: BsModalRef;

  constructor(
    public authService: AuthService,
    private alertify: AlertifyService,
    private groupService: GroupService,
    private modalService: BsModalService,
    private studentGroupAttendance: StudentGroupAttendanceService) { }

    openModal(template: TemplateRef<any>) {
    this.bsModalRef = this.modalService.show(template);
  }

  joinGroup(groupId: number) {
    this.groupService.joinGroup({groupId: groupId})
      .subscribe(() => {
        this.alertify.success('your request for joining the group has been sent to group Admin');
      },
      error => this.alertify.error(error));
  }

  joinStudentGroup(groupId: number) {
    this.studentGroupAttendance.joinStudentGroupAttendance({groupId: groupId})
      .subscribe(() => {
        this.alertify.success('your request for joining the group has been sent to group Admin');
      },
      error => this.alertify.error(error));
  }
}
