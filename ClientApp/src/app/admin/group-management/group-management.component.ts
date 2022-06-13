import { Component, OnInit } from '@angular/core';
import { AdminService } from './../../_services/admin.service';
import { AlertifyService } from './../../_services/alertify.service';

@Component({
  selector: 'app-group-management',
  templateUrl: './group-management.component.html',
  styleUrls: ['./group-management.component.css']
})
export class GroupManagementComponent implements OnInit {

  groups: any[] = [];

  constructor(
    private alertify: AlertifyService,
    private adminService: AdminService
    ) { }

  ngOnInit() {
    this.getGroupsForApproval();
  }
  
  getGroupsForApproval() {
    this.adminService.getGroupForApproval()
      .subscribe(groups => this.groups = groups, 
       error => {
          this.alertify.error(error);
      });
  }

  approvegroup(groupId) {
    this.adminService.approveGroup(groupId)
      .subscribe(() => {
        this.groups.splice(this.groups.findIndex(g => g.id === groupId), 1)
        this.alertify.success('Successfully aproved the group request')        
      }, error => {
          this.alertify.error(error)
      });
  }

  rejectgroup(groupId) {
    this.adminService.rejectGroup(groupId)
      .subscribe(() => {
        this.groups.splice(this.groups.findIndex(g => g.id === groupId), 1);
        this.alertify.success('Successfully reject the group approval');
      },error => {
          this.alertify.error(error);
      });
  }
}
