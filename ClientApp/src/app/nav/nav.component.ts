import { Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { AlertifyService } from '../_services/alertify.service';
import { NotificationService } from './../_services/notification.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  
  modalRef: BsModalRef;
  
  model: any = {};
  photoUrl: string;
  notifications: any[] = [];

  constructor(
    private router: Router,    
    public authService: AuthService, 
    private alertify: AlertifyService,    
    private modalService: BsModalService,
    private notificationService: NotificationService) { }

  ngOnInit() {

    this.authService.currentPhotoUrl
      .subscribe(photoUrl => this.photoUrl = photoUrl);
    
    this.notificationService.getNotifications()
      .subscribe(notifications => this.notifications = notifications);
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      this.alertify.success('Logged in successfully');
      this.model.username = '',
      this.model.password = ''
    }, error => {
      this.alertify.error(error);
    }, () => {
      this.router.navigate(['/members']);
    });
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !!token;
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.authService.decodedToken = null;
    this.authService.currentUser = null;
    this.alertify.message('logged out');
    this.router.navigate(['/home']);
  }
}
