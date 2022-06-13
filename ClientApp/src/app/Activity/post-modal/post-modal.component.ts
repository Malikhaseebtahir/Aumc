import { BsModalRef } from 'ngx-bootstrap';
import { Component, OnInit } from '@angular/core';
import { PostService } from './../../_services/post.service';
import { AuthService } from './../../_services/auth.service';
import { keyValuePairs } from './../../_models/PostCategory';
import { AlertifyService } from './../../_services/alertify.service';

@Component({
  selector: 'app-post-modal',
  templateUrl: './post-modal.component.html',
  styleUrls: ['./post-modal.component.css']
})
export class PostModalComponent implements OnInit {

  onClose: any;
  userId: number;
  title: string;
  post: any = {};
  groupId: number;
  closeBtnName: string;
  postCaterogies: keyValuePairs;

  constructor(
    public bsModalRef: BsModalRef,
    public authService: AuthService,
    private postService: PostService,
    private alertify: AlertifyService) { }
  
  ngOnInit() {
    this.postService.getPostCategories()
      .subscribe(pCategories => this.postCaterogies = pCategories);
    
    this.post.groupId = this.groupId;
  }

  submit() {
    this.postService.createPost(this.post)
      .subscribe(post => {
        this.alertify.success('New post addedd successfully')
        this.onClose(post);
      },
      error => this.alertify.error(error))
    this.bsModalRef.hide();
  }
}
