import { Component, OnInit, Input } from '@angular/core';
import { AlertifyService } from './../../_services/alertify.service';
import { PendingRequestService } from 'src/app/_services/pending-request.service';
import { StudentGroupAttendanceService } from 'src/app/_services/student-group-attendance.service';

@Component({
  selector: 'app-pending-groups-management',
  templateUrl: './pending-groups-management.component.html',
  styleUrls: ['./pending-groups-management.component.css']
})
export class PendingGroupsManagementComponent implements OnInit {
  
  @Input('group') group: any = {};

  attendanceDetails: any = {};

  pendingGroups: any[] = [];

  constructor(
    private alertify: AlertifyService,
    private pendingRequestService: PendingRequestService,
    private studentGroupAttendanceService: StudentGroupAttendanceService) { }

  ngOnInit() {
  }

  approveRequest(groupId: number, userId: number) {
    this.attendanceDetails.groupId = groupId;
    this.attendanceDetails.userId = userId;

    this.removeGroupFromList(groupId, userId);

    this.pendingRequestService.approveRequest(this.attendanceDetails)
      .subscribe(() => {
        this.alertify.success('successfully approved')
      },
      () => this.alertify.error('somwthing went wrong'));
  }
  
  rejectRequest(groupId: number, userId: number) {
    this.attendanceDetails.groupId = groupId;
    this.attendanceDetails.userId = userId;

    this.removeGroupFromList(groupId, userId);

    this.pendingRequestService.rejectRequest(this.attendanceDetails)
      .subscribe(() => {
        this.alertify.success('successfully reject');
      },
      () => this.alertify.error('something went wrong'));
  }

  approveStudnetGroupRequest(groupId: number, userId: number) {
    this.attendanceDetails.groupId = groupId;
    this.attendanceDetails.userId = userId;
    
    this.studentGroupAttendanceService.approveStudentGroupAttendance(this.attendanceDetails)
      .subscribe(() => {
        this.alertify.success('successfully reject');
      },
      () => this.alertify.error('something went wrong'));
}

  rejectStudentGroupRequest(groupId: number, userId: number) {
    this.attendanceDetails.groupId = groupId;
    this.attendanceDetails.userId = userId;

    this.studentGroupAttendanceService.rejectStudentGroupAttendance(this.attendanceDetails)
      .subscribe(() => {
        this.alertify.success('successfully reject');
      },
      () => this.alertify.error('something went wrong'));
}

  private removeGroupFromList(groupId: number, userId: number) {
    const index = this.pendingGroups.findIndex(pg => pg.group.id == groupId && pg.user.id == userId);
    this.pendingGroups.splice(index, 1);
  }
}
