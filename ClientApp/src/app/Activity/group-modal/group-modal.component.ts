import { AlertifyService } from './../../_services/alertify.service';
import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap';
import { GroupService } from 'src/app/_services/group.service';
import { SaveGroup } from 'src/app/_models/SaveGroup';

@Component({
  selector: 'app-group-modal',
  templateUrl: './group-modal.component.html',
  styleUrls: ['./group-modal.component.css']
})
export class GroupModalComponent implements OnInit {
  groupTypes;
  group: SaveGroup = {
    id: 0,
    className: '',
    section: '',
    subject: '',
    room: '',
    groupTypeId: 0,
    description: ''
  };
  title: string;
  closeBtnName: string;
  list: any[] = [];
  
  constructor(
    public bsModalRef: BsModalRef,
    private alert: AlertifyService,
    private groupService: GroupService
    ) {}
  
  ngOnInit(): void {
    this.groupService.getGroupTypes()
      .subscribe(groupTypes => this.groupTypes = groupTypes);
  }

  submit() {
    this.groupService.createGroup(this.group)
      .subscribe(() => {
        this.alert.success("you request for creating new group has bee sent successfully") 
        this.bsModalRef.hide();       
      }, error => {
        this.alert.error(error);
      });
  }
}