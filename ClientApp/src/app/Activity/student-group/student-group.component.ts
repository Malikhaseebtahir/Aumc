import { StudentGroupPostCommentService } from './../../_services/student-group-post-comment.service';
import { StudentGroupCommentService } from './../../_services/student-group-comment.service';
import { AuthService } from './../../_services/auth.service';
import { StudentGroupAttendanceService } from 'src/app/_services/student-group-attendance.service';
import { StudentGroupPostService } from './../../_services/student-group-post.service';
import { AlertifyService } from './../../_services/alertify.service';
import { StudentGroupNewsLetterService } from './../../_services/student-group-news-letter.service';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { StudentGroupService } from 'src/app/_services/student-group.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap';

@Component({
  selector: 'app-student-group',
  templateUrl: './student-group.component.html',
  styleUrls: ['./student-group.component.css']
})
export class StudentGroupComponent implements OnInit {
  
  oneAtATime: boolean = true;

  createNews: any = {
    id: 0,
    title: '',
    description: ''
  };

  postComment: any = {}
  comment: any = {};

  groupId: number;
  groupPost: any = {};
  group: any = {};

  modalRef: BsModalRef;
  groupNews: any[] = [];
  groupPosts: any[] = [];
  groupUsers: any[] = [];
  pendingPosts: any[] = [];
  postComments: any[] = [];
  groupComments: any[] = [];
  postDetail: any;

  constructor(
    private router: ActivatedRoute,
    public authSerivce: AuthService,
    private alertify: AlertifyService,
    private modalService: BsModalService,
    private studentGroupService: StudentGroupService,
    private commentService: StudentGroupCommentService,
    private newLetterService: StudentGroupNewsLetterService,
    private studentGroupPostService: StudentGroupPostService,
    private postCommentService: StudentGroupPostCommentService,
    private studentGroupAttendanceService: StudentGroupAttendanceService) { }

  ngOnInit() {
    this.groupId = +this.router.snapshot.paramMap.get('id');
    
    if (this.groupId) { 
      this.studentGroupService.getStudentGroup(this.groupId)
        .subscribe(group => this.group = group)

      this.studentGroupAttendanceService.getStudentGroupUsers(this.groupId)
        .subscribe(groupUsers => this.groupUsers = groupUsers)
      
      this.commentService.getComments(this.groupId)
        .subscribe(groupComments => this.groupComments = groupComments)

      this.studentGroupPostService.getAllStudentGroupPost(this.groupId)
        .subscribe(posts => this.groupPosts = posts)
      
      this.studentGroupPostService.getAllStudentGroupPostsForApproval(this.groupId)
        .subscribe(pendingPosts => this.pendingPosts = pendingPosts)
      
      this.newLetterService.getAllStudentGroupNews(this.groupId)
        .subscribe(groupNews => this.groupNews = groupNews);
    }
  }

  openModal(news: TemplateRef<any>) {
    this.createNews = {
      id: 0,
      title: '',
      description: ''
    };
    this.modalRef = this.modalService.show(news);
  }

  openModalWithPostDetails(template: TemplateRef<any>, postId: number) {
    this.group.studentGroupPosts.forEach(element => {
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

  openModalForNewsUpdate(updateNews: TemplateRef<any>, news: any) {
    this.createNews = news
    this.modalRef = this.modalService.show(updateNews);
  }

  saveNews() {
    if (this.createNews.id == 0) {
      this.createNews.groupId = this.groupId;
      this.newLetterService.createNews(this.createNews)
        .subscribe(news => {
          this.alertify.success('news addedd');
          this.modalRef.hide();
          this.groupNews.push(news);
          this.createNews = {};
        },
        () => this.alertify.error('something went wrong'));
      } else {
        const index = this.groupNews.findIndex(n => n.id == this.createNews.id);

        this.newLetterService.updateNews(this.createNews)
          .subscribe(news => {
            this.groupNews.splice(index, 1, news);
            this.modalRef.hide();
          },
          () => this.alertify.error('something went wrong'));
    }
  }

  removeNews(newsId) {
    this.alertify.confirm('Are you sure you want to delete this news ?', () => {
      const index = this.groupNews.findIndex(n => n.id == newsId);

      this.newLetterService.deleteStudentGroupNews(newsId)
        .subscribe(() => {
          this.groupNews.splice(index, 1);
          this.alertify.success('news deleted');
        },
        () => this.alertify.error('something went wrong'));
    });
  }

  openModalForNewPost(newPost: TemplateRef<any>) {
    this.modalRef = this.modalService.show(newPost);
  }
  
  openModalForPrivacy(privacy: TemplateRef<any>) {
    this.modalRef = this.modalService.show(privacy);
  }

  addPost() {
    this.groupPost.groupId = this.groupId;

    this.studentGroupPostService.createStudentGroupPost(this.groupPost)
      .subscribe(() => {
        this.modalRef.hide();
        this.alertify.success('your request for adding new post has been send to group admin');
      },
      () => this.alertify.error('something went wrong'));
  }

  addComment() {
    this.comment.groupId = +this.group.id;

    this.commentService.addComment(this.comment)
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
}
