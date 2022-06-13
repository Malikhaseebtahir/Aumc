import { AlertifyService } from './../../_services/alertify.service';
import { Component, OnInit } from '@angular/core';
import { GroupService } from 'src/app/_services/group.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { GroupModalComponent } from '../group-modal/group-modal.component';
import { PendingRequestService } from 'src/app/_services/pending-request.service';

@Component({
  selector: 'app-teacher-activity',
  templateUrl: './teacher-activity.component.html',
  styleUrls: ['./teacher-activity.component.css']
})
export class TeacherActivityComponent implements OnInit {
  
  allGroups;
  bsModalRef: BsModalRef;
  groups: Object;
  pendingRequests: any[] = [];

  constructor(
    private alertify: AlertifyService,
    private groupService: GroupService,
    private modalService: BsModalService,
    private pendingRequestService: PendingRequestService) { }

  ngOnInit() {
    this.groupService.getGroups()
      .subscribe(groups => this.allGroups = groups);

    this.groupService.getGroupsByUser()
      .subscribe(groups => this.groups = groups);
    
    this.pendingRequestService.getPendingRequestForAdmin()
      .subscribe(pendingRequests => {
        console.log(pendingRequests);
        this.pendingRequests = pendingRequests;
      })
  }

  openModalWithComponent() {
    const initialState = {
      title: 'Create New Group'
    };
    this.bsModalRef = this.modalService.show(GroupModalComponent, {initialState});
    this.bsModalRef.content.closeBtnName = 'Close';
  }

  joinGroup(groupId: number) {
    this.groupService.joinGroup({groupId: groupId})
      .subscribe(() => {
        this.alertify.success('you join the group');
      },
      error => this.alertify.error(error));
  }
}
