import { AlertifyService } from './../../_services/alertify.service';
import { AdminService } from './../../_services/admin.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-student-group-management',
  templateUrl: './student-group-management.component.html',
  styleUrls: ['./student-group-management.component.css']
})
export class StudentGroupManagementComponent implements OnInit {
  
  studentGroups: any[] = [];

  constructor(
    private alrtify: AlertifyService,
    private adminService: AdminService) { }

  ngOnInit() {
    this.adminService.getStudentGroupsForApproval()
      .subscribe(groups => {
        this.studentGroups = groups;
        console.log(groups);
      })
  }

  approvegroup(groupId: number) {
    
    const index = this.studentGroups.findIndex(g => g.id == groupId);

    this.adminService.approveStudentGroup(groupId)
      .subscribe(() => {
        this.studentGroups.splice(index, 1);
        this.alrtify.success('group approved');
      },
      () => this.alrtify.error('something went wrong'))
  }

  rejectgroup(groupId: number) {
    const index = this.studentGroups.findIndex(g => g.id == groupId);

    this.adminService.rejectStudentGroup(groupId)
      .subscribe(() => {
        this.studentGroups.splice(index, 1);
        this.alrtify.success('group rejected');
      },
      () => this.alrtify.error('something went wrong'));
  }
}
