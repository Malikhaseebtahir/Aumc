import { User } from '../../_models/user';
import { ActivatedRoute } from '@angular/router';
import { UserService } from './../../_services/user.service';
import { AuthService } from './../../_services/auth.service';
import { AlertifyService } from './../../_services/alertify.service';
import { FollowingService } from './../../_services/following.service';
import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { TabsetComponent, BsModalService, BsModalRef } from 'ngx-bootstrap';
import { NgxGalleryOptions, NgxGalleryImage, NgxGalleryAnimation } from 'ngx-gallery';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {

  @ViewChild('memberTabs', {static: true}) memberTabs: TabsetComponent;

  modalRef: BsModalRef;
  
  user: User;
  report: any = {};
  galleryImages: NgxGalleryImage[];
  galleryOptions: NgxGalleryOptions[];

  constructor(
    private route: ActivatedRoute,
    private aurhService: AuthService,
    private userService: UserService,
    private alertify: AlertifyService,
    private modalService: BsModalService,    
    private followService: FollowingService) { }

  ngOnInit() {
    this.route.data.subscribe(data => this.user = data['user']);

    this.route.queryParams.subscribe(params => {
      const selectedTab = params['tab'];
      this.memberTabs.tabs[selectedTab > 0 ? selectedTab : 0].active = true;
    });

    this.galleryOptions = [
      {
        width: '500px',
        height: '500px',
        imagePercent: 100,
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide,
        preview: false
      }
    ];
    this.galleryImages = this.getImages();
  }

  getImages() {
    const imageUrls = [];
    for (let i = 0; i < this.user.photos.length; i++) {
      imageUrls.push({
        small: this.user.photos[i].url,
        medium: this.user.photos[i].url,
        big: this.user.photos[i].url,
        description: this.user.photos[i].description
      });
    }
    return imageUrls;
  }

  selectTab(tabId: number) {
    this.memberTabs.tabs[tabId].active = true;
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  followUser() {
    this.followService.followUser(this.user.id)
      .subscribe(() => {
        this.alertify.success('you start following this user')
      },
      error => this.alertify.error(error));
  }

  reportUser() {
    this.report.ReporteeId = this.user.id;
    this.report.ReporterId = +this.aurhService.decodedToken.nameid;

    this.userService.reportUser(this.report)
      .subscribe(() => {
        this.alertify.success("your report for this user has been send to Mam Raheela.")
      },
      () => this.alertify.error('something went wrong'));

    this.modalRef.hide()
  }
}
