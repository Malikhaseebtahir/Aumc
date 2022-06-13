import { User } from '../../_models/user';
import { AuthService } from '../../_services/auth.service';
import { UserService } from '../../_services/user.service';
import { AlertifyService } from '../../_services/alertify.service';
import { Component, Input, Output, EventEmitter } from '@angular/core';
import { FollowingService } from './../../_services/following.service';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent {

  @Input() user: User;
  @Input('unFollowLink') unFollowLink: boolean;
  @Input('unLikeLink') unLikeLink: boolean;
  @Input('like') like: boolean;

  @Output('removeUserId') removeUserId = new EventEmitter();

  constructor(
    private authService: AuthService, 
    private userService: UserService, 
    private alertify: AlertifyService,    
    private followingService: FollowingService) { }

  sendLike(id: number) {
    this.userService.sendLike(this.authService.decodedToken.nameid, id).subscribe(() => {
      this.alertify.success('You have liked: ' + this.user.knownAs);
    }, error => {
      this.alertify.error(error);
    });
  }

  unFollowUser(userId: number) {
    this.followingService.unFollowUser(userId)
      .subscribe(() => {
        this.alertify.success('You unfollow this user');
        this.removeUserId.emit(userId);
      },
      error => this.alertify.error(error));
  }

  removeLike(userId: number) {
    this.userService.removeLike(userId)
      .subscribe(() => {
        this.alertify.success('successfully dislike the user');
        this.removeUserId.emit(userId);
      },
      error => this.alertify.error(error));
  }
}
