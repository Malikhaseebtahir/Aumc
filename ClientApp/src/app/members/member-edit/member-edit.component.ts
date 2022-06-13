import { NgForm } from '@angular/forms';
import { User } from '../../_models/user';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalService, BsModalRef } from 'ngx-bootstrap';
import { AuthService } from '../../_services/auth.service';
import { UserService } from '../../_services/user.service';
import { AlertifyService } from '../../_services/alertify.service';
import { Component, OnInit, ViewChild, HostListener, TemplateRef } from '@angular/core';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {
  
  @ViewChild('editForm', {static: true}) editForm: NgForm;
  
  modalRef: BsModalRef;
  
  user: any = {};
  photoUrl: string;
  interests: any[] = [];
  newInterest: any = {};
  
  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any) {
    if (this.editForm.dirty) {
      $event.returnValue = true;
    }
  }

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private authService: AuthService,     
    private userService: UserService,
    private alertify: AlertifyService,    
    private modalService: BsModalService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.user = data['user'];
      this.setUser(data['user']);
    });
    
    this.userService.getInterests()
      .subscribe(interests => this.interests = interests);

      this.authService.currentPhotoUrl.subscribe(photoUrl => this.photoUrl = photoUrl);
  }

  updateUser() {
    this.userService.updateUser(this.authService.decodedToken.nameid, this.user).subscribe(next => {
      this.alertify.success('Profile updated successfully');
      this.router.navigate(['/members']);
      this.editForm.reset(this.user);
    }, error => {
      this.alertify.error(error);
    });
  }

  updateMainPhoto(photoUrl) {
    this.user.photoUrl = photoUrl;
  }

  setUser(user: User) {
    this.user.id = user.id,
    this.user.className = user.className,
    this.user.introduction = user.introduction,
    this.user.lookingFor = user.lookingFor;
    this.user.address.city = user.address.city,
    this.user.address.province = user.address.province,
    this.user.address.country = user.address.country
    this.user.interestId = user.interest != null ? user.interest.id : 0; 
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  addNewInterest() {
    this.userService.createInterest(this.newInterest)
      .subscribe(() => this.alertify.success('your request for interest approval has been sent to admin'),
      
      () => this.alertify.error('sorry something went wrong'));
  
    this.modalRef.hide()
  }
}
