import { StudentGroupAttendanceService } from './../../_services/student-group-attendance.service';
import { AlertifyService } from './../../_services/alertify.service';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap';
import { GroupService } from 'src/app/_services/group.service';
import { GroupModalComponent } from '../group-modal/group-modal.component';
import { StudentGroupService } from 'src/app/_services/student-group.service';

@Component({
  selector: 'app-student-activity',
  templateUrl: './student-activity.component.html',
  styleUrls: ['./student-activity.component.css']
})
export class StudentActivityComponent implements OnInit {
  
  bsModalRef: BsModalRef;

  groups: any[] = [];
  studentGroup: any = {}
  allGroups: any[] = [];
  studentGroups: any[] = [];
  groupOfCreators: any[];
  pendingRequests: any[] = [];
  
  constructor(
    private groupService: GroupService,
    private alertify: AlertifyService,
    private studentGroupAttendance: StudentGroupAttendanceService,
    private modalService: BsModalService,
    private studentGroupService: StudentGroupService) { }

  ngOnInit() {

    this.studentGroupService.getAllStudentGroups()
      .subscribe(groups => this.studentGroups = groups);
    
    this.studentGroupAttendance.getPendingRequestForAdmin()
      .subscribe(pendingRequests => this.pendingRequests = pendingRequests)

    this.studentGroupService.getAllStudentGroupForCreator()
      .subscribe(groups => this.groupOfCreators = groups)

    this.groupService.getGroups()
      .subscribe(groups => this.allGroups = groups);

    this.groupService.getGroupsByUser()
      .subscribe(groups => this.groups = groups);
  }

  openModalWithComponent() {
    const initialState = {
      title: 'Create New Group'
    };
    this.bsModalRef = this.modalService.show(GroupModalComponent, {initialState});
    this.bsModalRef.content.closeBtnName = 'Close';
  }
 
  openModal(template: TemplateRef<any>) {
    this.bsModalRef = this.modalService.show(template);
  }

  saveStudentGroup() {
    this.studentGroupService.createStudentGroup(this.studentGroup)
      .subscribe(() => {
        this.alertify.success('your request for creating this group has been send to Mam Raheela');
        this.bsModalRef.hide();
      },
      () => this.alertify.error('something went wrong'));
  }
}
