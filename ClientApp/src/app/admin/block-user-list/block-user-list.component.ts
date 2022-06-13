import { Component, OnInit } from '@angular/core';
import { AdminService } from './../../_services/admin.service';

@Component({
  selector: 'app-block-user-list',
  templateUrl: './block-user-list.component.html',
  styleUrls: ['./block-user-list.component.css']
})
export class BlockUserListComponent implements OnInit {
  
  users: any[] = [];

  constructor(private adminService: AdminService) { }

  ngOnInit() {
    this.adminService.getListOfBlockUser()
      .subscribe(users => this.users = users);
  }

}
