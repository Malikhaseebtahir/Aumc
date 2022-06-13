import { StudyGroupPostCommentService } from './../../_services/study-group-post-comment.service';
import { GroupNewsLetterService } from './../../_services/group-news-letter.service';
import { PostModalComponent } from './../post-modal/post-modal.component';
import { AlertifyService } from './../../_services/alertify.service';
import { GroupService } from 'src/app/_services/group.service';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { PostService } from './../../_services/post.service';
import { AuthService } from './../../_services/auth.service';
import { environment } from 'src/environments/environment';
import { BsModalService, BsModalRef } from 'ngx-bootstrap';
import { ActivatedRoute, Router } from '@angular/router';
import { Post } from './../../_models/Post';
import { FileUploader } from 'ng2-file-upload';

import { forkJoin } from 'rxjs';
import * as jsPDF from 'jspdf';

@Component({
  selector: 'app-group',
  templateUrl: './group.component.html',
  styleUrls: ['./group.component.css']
})
export class GroupComponent implements OnInit {

  // Accordion
  oneAtATime: boolean = true

  createNews: any = {
    id: 0,
    title: '',
    description: ''
  }
  
  modalRef: BsModalRef;
  uploader: FileUploader;
  baseUrl = environment.apiUrl;
  hasBaseDropZoneOver = false;

  postId: any;
  group: any = {};
  postDetail: any;
  groupId: number;
  comment: any = {};
  postComment: any = {}
  groupUsers: any[] = [];
  groupComments: any[] = [];
  groupNews: any[] = [];
  postComments: any[] = [];

  constructor(
    private router: Router,
    private route: ActivatedRoute,   
    public authService: AuthService,
    private postService: PostService,
    private alertify: AlertifyService,
    private groupService: GroupService,
    private modalService: BsModalService,
    private groupNewsService: GroupNewsLetterService,
    private postCommentService: StudyGroupPostCommentService,
    ) { }

  ngOnInit() {
    this.groupId = +this.route.snapshot.paramMap.get('id');

    if (this.groupId) { 
      forkJoin(
        this.groupService.getGroup(this.groupId),
        this.groupService.getComments(this.groupId),
        this.groupService.getGroupUsers(this.groupId),
        this.groupNewsService.getGroupNews(this.groupId)
      ).subscribe(data => {
          this.group = data[0],
          this.groupComments = data[1],
          this.groupUsers = data[2],
          this.groupNews = data[3]
      },
      () => this.alertify.error('something went wrong'));
    }
  }

  fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }  

  initializeUploader(postId) {
    this.group.posts.forEach(element => {
      if (element.id ==  postId)
        this.postId = element.id;
    });

    this.uploader = new FileUploader({
      url: this.baseUrl + this.postId + '/posts',
      authToken: 'Bearer ' + localStorage.getItem('token'),
      isHTML5: true,
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
    });  
  }  

  openModalWithPostDetails(template: TemplateRef<any>, postId: number) {
    this.group.posts.forEach(element => {
      if (element.id ==  postId)
        this.postDetail = element;
    });

    this.postCommentService.getPostComments(postId)
      .subscribe(postComments => this.postComments = postComments);

    this.modalRef = this.modalService.show(
      template,
      Object.assign({}, { class: 'gray modal-lg' })
    );
  }

  openModalForPrivacy(privacy: TemplateRef<any>) {
    this.modalRef = this.modalService.show(privacy);
  }

  openModalForNews(news: TemplateRef<any>) {
    this.createNews = {
      id: 0,
      title: '',
      description: ''
    };
    this.modalRef = this.modalService.show(news);    
  }

  openModalWithComponent() {
    const initialState = {
      title: 'Add new post',
      userId: this.group.user.id,
      groupId: this.groupId
    };

    this.modalRef = this.modalService.show(PostModalComponent, {initialState});
    this.modalRef.content.closeBtnName = 'Close';
    this.modalRef.content.onClose = (post:Post) => this.group.posts.push(post);
  }

  openModalWithClass(fileTemplate: TemplateRef<any>, postId) {
    this.initializeUploader(postId);

    this.modalRef = this.modalService.show(
      fileTemplate,
      Object.assign({}, { class: 'gray modal-lg' })
    );
  }

  openModalForNewsUpdate(updateNews: any, news: any) {
    this.createNews = news
    this.modalRef = this.modalService.show(updateNews);    
  }

  submit() {
    console.log(this.postDetail);
    // this.postService.updatePost(this.postDetail)
    //   .subscribe(() => {
    //     this.alertify.success('Post successfully updated');
    //   },
    //   error => this.alertify.error(error));
  }

  onDeleteGroup() {
    this.alertify.confirm('Are you sure you want to delete this group', () => {
      this.groupService.deleteGroup(this.group.id)
        .subscribe(() => {
          this.router.navigate(['/student']);
          this.alertify.success('successfully deleted the group');       
        },
        error => this.alertify.error(error));
    });
  }

  onDelete() {
    this.alertify.confirm('Are your sure you want to delete this post', () => {
      this.postService.deletePost(this.postDetail.id)
        .subscribe(() => {
          let post;
          this.group.posts.forEach(element => {
            if (element.id ==  this.postDetail.id)
              post = element;
          });
          this.group.posts.splice(this.group.posts.indexOf(post), 1)
          this.alertify.success('successfully delete thie post');
        },
        error => this.alertify.error(error));
    });

    this.modalRef.hide();
  }

  addComment() {
    this.comment.groupId = +this.group.id;

    this.groupService.addComment(this.comment)
      .subscribe(comment => {
        this.alertify.success('comment added successfully');
        this.comment.message = '';
        this.groupComments.push(comment);
      },
      () => this.alertify.error('Something went wrong'));
  }

  addPostComment(postId: number) {
    this.postComment.postId = postId;

    this.postCommentService.addPostComment(this.postComment)
      .subscribe(comment => {
        this.alertify.success('comment addedd');
        this.postComment.message = '';
        this.postComments.push(comment);
      },
      () => this.alertify.error('something went wrong'));
  }

  deleteComment(commentId: number) {
    let index = this.groupComments.findIndex(c => c.id == commentId);
    
    this.alertify.confirm('Are you sure you want to delete this comment', () => {
      this.groupComments.splice(index,1);
      this.groupService.deleteComment(commentId)
        .subscribe(() => {
          this.alertify.success('comment deleted')          
        },
        () => this.alertify.error('something went wrong'));
    });
  }
  
  deletePostComment(postCommentId: number) {
    let index = this.postComments.findIndex(c => c.id == postCommentId);
    
    this.alertify.confirm('Are you sure you want to delete this comment', () => {
      this.postComments.splice(index,1);
      this.postCommentService.deletePostComment(postCommentId)
        .subscribe(() => {
          this.alertify.success('comment deleted')          
        },
        () => this.alertify.error('something went wrong'));
    });
  }

  saveNews() {
    this.createNews.groupId = this.groupId;
    if (this.createNews.id == 0) {
      this.groupNewsService.createGroupNews(this.createNews)
        .subscribe(news => {
          this.alertify.success('news added');
          this.groupNews.push(news);
          this.modalRef.hide();
          this.createNews = {};
        },
        () => this.alertify.error('something went wrong'));
    } else {
        const index = this.groupNews.findIndex(n => n.id == this.createNews.id);

        this.groupNewsService.updateGroupNews(this.createNews)
          .subscribe(news => {
            this.groupNews.splice(index, 1, news);
            this.modalRef.hide();
          },
          () => this.alertify.error('something went wrong'));
    }
  }

  removeNews(newsId: number) {
    this.alertify.confirm('Are you sure you want to delete this news ?', () => {
      const index = this.groupNews.findIndex(n => n.id == newsId);

      this.groupNewsService.deleteGroupNews(newsId)
        .subscribe(() => {
          this.groupNews.splice(index, 1);
          this.alertify.success('news deleted');
        },
        () => this.alertify.error('something went wrong'));
    });
  } 

  downloadPdf(postUrl: string, postName: string) {
    const doc = new jsPDF();
    doc.text(postUrl, 10, 10);

    doc.save(postName + '.pdf');
  }
}