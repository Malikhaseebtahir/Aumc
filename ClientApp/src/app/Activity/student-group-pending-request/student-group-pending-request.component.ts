import { AlertifyService } from './../../_services/alertify.service';
import { StudentGroupPostService } from './../../_services/student-group-post.service';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-student-group-pending-request',
  templateUrl: './student-group-pending-request.component.html',
  styleUrls: ['./student-group-pending-request.component.css']
})
export class StudentGroupPendingRequestComponent implements OnInit {
  
  groupId: number;
  pendingPosts: any[] = [];

  constructor( 
    private route: ActivatedRoute,
    private alertify: AlertifyService,
    private PostService: StudentGroupPostService) { }

  ngOnInit() {
    this.groupId = +this.route.snapshot.paramMap.get('id');

    if (this.groupId) {
      this.PostService.getAllStudentGroupPostsForApproval(this.groupId)
        .subscribe(pendingPosts => {
          console.log(pendingPosts);
          this.pendingPosts = pendingPosts;
        })
    }
  }

  approveRequest(postId: number) {
    
    const index = this.pendingPosts.findIndex(p => p.id == postId);

    this.PostService.approveStudentGroupPost(postId)
      .subscribe(() => {
        this.pendingPosts.splice(index, 1);
        this.alertify.success('post approved');
      },
      () => this.alertify.error('something went wrong'));
  }

  rejectRequest(postId: number) {

    const index = this.pendingPosts.findIndex(p => p.id == postId);

    this.PostService.rejectStudentGroupPost(postId)
      .subscribe(() => {
        this.pendingPosts.splice(index, 1);
        this.alertify.success('post rejected');
      },
      () => this.alertify.error('something went wrong'))
  }

}
