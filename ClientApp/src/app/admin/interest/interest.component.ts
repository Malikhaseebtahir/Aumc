import { Component, OnInit } from '@angular/core';
import { Interest } from 'src/app/_models/Interest';
import { AdminService } from './../../_services/admin.service';
import { AlertifyService } from './../../_services/alertify.service';

@Component({
  selector: 'app-interest',
  templateUrl: './interest.component.html',
  styleUrls: ['./interest.component.css']
})
export class InterestComponent implements OnInit {
  
  interests: Interest[] = [];

  constructor(
    private alertify: AlertifyService,
    private adminService: AdminService,
    ) { }

  ngOnInit() {
    this.adminService.getInterestForApproval()
      .subscribe(interests => this.interests = interests);
  }

  onApprove(interestId: number) {
    let interest = this.interests.find(i => i.id == interestId);
    this.interests.splice(this.interests.indexOf(interest), 1);

    this.adminService.approveInterest(interestId)
      .subscribe(() => this.alertify.success('Approved'),
      
      error => {
        this.alertify.error(error);
        this.interests.push(interest);
      })
  }
}
