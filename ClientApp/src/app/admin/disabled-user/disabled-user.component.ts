import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { AdminService } from './../../_services/admin.service';
import { AlertifyService } from './../../_services/alertify.service';

@Component({
  selector: 'app-disabled-user',
  templateUrl: './disabled-user.component.html',
  styleUrls: ['./disabled-user.component.css']
})
export class DisabledUserComponent implements OnInit {
  
  modalRef: BsModalRef;
  users: any[] = [];

  constructor(
    private alertify: AlertifyService,
    private adminService: AdminService,    
    private modalService: BsModalService) { }

  ngOnInit() { 
    this.adminService.getDisabledUser()
      .subscribe(users => this.users = users);
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  enableUser(userId: number) {
    this.modalRef.hide();

    let user = this.users.find(u => u.id == userId);
    this.users.splice(user, 1);

    this.adminService.enableUser(userId)
      .subscribe(() => {
        this.alertify.success('Successfully enable the user');
      })
      error => this.alertify.error(error)
  }
}
