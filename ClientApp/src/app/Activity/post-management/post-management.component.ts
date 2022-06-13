import { Component, OnInit } from '@angular/core';
import { AdminService } from './../../_services/admin.service';
import { AlertifyService } from './../../_services/alertify.service';

@Component({
  selector: 'app-post-management',
  templateUrl: './post-management.component.html',
  styleUrls: ['./post-management.component.css']
})
export class PostManagementComponent implements OnInit {
  posts: any;

  constructor(
    private alertify: AlertifyService,
    private adminService: AdminService) { }

  ngOnInit() {
    this.adminService.getPostForApproval()
      .subscribe(posts => this.posts = posts);
  }

  approvePost(postId: number) {
    this.adminService.approvePost(postId)
      .subscribe(() => {
        this.posts.splice(this.posts.findIndex(p => p.id === postId), 1)
        this.alertify.success('Successfully approved the post');
      },
      error => this.alertify.error(error));
  }

  rejectPost(postId: number) {
    this.adminService.rejectPost(postId)
      .subscribe(() => {
        this.posts.splice(this.posts.findIndex(p => p.id === postId), 1)
        this.alertify.success('Successfully rejected the post');
      },
      error => this.alertify.error(error));
  }

}
