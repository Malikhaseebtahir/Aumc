import { StudentGroupService } from 'src/app/_services/student-group.service';
import { AlertifyService } from './../../_services/alertify.service';
import { Component, OnInit } from '@angular/core';
import { PendingRequestService } from 'src/app/_services/pending-request.service';

@Component({
  selector: 'app-pending-request',
  templateUrl: './pending-request.component.html',
  styleUrls: ['./pending-request.component.css']
})
export class PendingRequestComponent implements OnInit {
  
  requests: any[] = [];

  constructor(
    private alertify: AlertifyService,
    private studentGroupService: StudentGroupService,
    private pendingService: PendingRequestService) { }

  ngOnInit() {
    this.pendingService.getPendingRequestForUser()
      .subscribe(requests => this.requests = requests);
  }
    
  loadStudyGroupPendingRequest() {
    this.pendingService.getPendingRequestForUser()
      .subscribe(requests => this.requests = requests);
  }

  loadStudentGroupPendingRequest() {
    this.studentGroupService.getAllPendingRequestForUser()
      .subscribe(requests => this.requests = requests);
  }

  cancelRequest(groupId: number) {
    const index = this.requests.findIndex(u => u.id == groupId);
    this.requests.splice(index -1, 1);

    this.pendingService.cancelRequest(groupId)
      .subscribe(() => this.alertify.success('successfully cancel the request'),
      error => this.alertify.error(error));
    }
    
  cancelStudentGroupRequest(groupId: number) {
    this.studentGroupService.deletAttendance(groupId)
      .subscribe(() => this.alertify.success('successfully cancel the request'),
       error => this.alertify.error(error));
  }
}
