import { Component, OnInit } from '@angular/core';
import { AlertifyService } from './../_services/alertify.service';
import { FollowingService } from './../_services/following.service';

@Component({
  selector: 'app-following',
  templateUrl: './following.component.html',
  styleUrls: ['./following.component.css']
})
export class FollowingComponent implements OnInit {

  users: any[] = [];

  constructor(
    private alertify: AlertifyService,
    private followingService: FollowingService) { }

  ngOnInit() {

    this.followingService.getAllFollowees()
      .subscribe(users => this.users = users,
      () => this.alertify.error('Something went wrong'));
  }

  removeUserFromList(userId: number) {
    const index = this.users.findIndex(u => u.follower.id == userId);
    this.users.splice(index, 1);
  }
}
