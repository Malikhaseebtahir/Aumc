import { DeviceSelectComponent } from './VideoCalling/settings/device-select.component';
import { VideoChatService } from './VideoCalling/services/videochat.service';
import { StorageService } from './VideoCalling/services/storage.service';
import { DeviceService } from './VideoCalling/services/device.service';
import { SettingsComponent } from './VideoCalling/settings/settings.component';
import { RoomsComponent } from './VideoCalling/rooms/rooms.component';
import { ParticipantsComponent } from './VideoCalling/participants/participants.component';
import { CameraComponent } from './VideoCalling/camera/camera.component';
import { ActivityIndicatorComponent } from './VideoCalling/activity-indicator/activity-indicator.component';
import { UniversityNewsService } from './_services/university-news.service';
import { UniversityNewsComponent } from './university-news/university-news.component';
import { StudentGroupPostCommentService } from './_services/student-group-post-comment.service';
import { StudyGroupPostCommentService } from './_services/study-group-post-comment.service';
import { StudentGroupCommentService } from './_services/student-group-comment.service';
import { GroupNewsLetterService } from './_services/group-news-letter.service';
import { StudentGroupAttendanceService } from './_services/student-group-attendance.service';
import { StudentGroupPendingRequestComponent } from './Activity/student-group-pending-request/student-group-pending-request.component';
import { StudentGroupPostService } from './_services/student-group-post.service';
import { StudentGroupNewsLetterService } from './_services/student-group-news-letter.service';
import { StudentGroupComponent } from './Activity/student-group/student-group.component';
import { PendingRequestService } from './_services/pending-request.service';
import { FollowingService } from './_services/following.service';
import { NotificationService } from './_services/notification.service';
import { GroupUsersComponent } from './Activity/group-users/group-users.component';
import { PostService } from './_services/post.service';
import { AccordionModule } from 'ngx-bootstrap/accordion';
import { PostModalComponent } from './Activity/post-modal/post-modal.component';
import { GroupComponent } from './Activity/group/group.component';
import { GroupModalComponent } from './Activity/group-modal/group-modal.component';
import { BrowserModule, HammerGestureConfig, HAMMER_GESTURE_CONFIG } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule, TabsModule, BsDatepickerModule, PaginationModule, ButtonsModule, ModalModule } from 'ngx-bootstrap';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { JwtModule } from '@auth0/angular-jwt';
import { NgxGalleryModule } from 'ngx-gallery';
import { FileUploadModule } from 'ng2-file-upload';
import { TimeAgoPipe } from 'time-ago-pipe';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { AuthService } from './_services/auth.service';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { AlertifyService } from './_services/alertify.service';
import { MemberListComponent } from './members/member-list/member-list.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { appRoutes } from './routes';
import { AuthGuard } from './_guards/auth.guard';
import { UserService } from './_services/user.service';
import { MemberCardComponent } from './members/member-card/member-card.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberDetailResolver } from './_resolvers/member-detail.resolver';
import { MemberListResolver } from './_resolvers/member-list.resolver';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { MemberEditResolver } from './_resolvers/member-edit.resolver';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';
import { PhotoEditorComponent } from './members/photo-editor/photo-editor.component';
import { ListsResolver } from './_resolvers/lists.resolver';
import { MessagesResolver } from './_resolvers/messages.resolver';
import { MemberMessagesComponent } from './members/member-messages/member-messages.component';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { HasRoleDirective } from './_directives/hasRole.directive';
import { UserManagementComponent } from './admin/user-management/user-management.component';
import { PhotoManagementComponent } from './admin/photo-management/photo-management.component';
import { AdminService } from './_services/admin.service';
import { RolesModalComponent } from './admin/roles-modal/roles-modal.component';
import { TeacherActivityComponent } from './Activity/teacher-activity/teacher-activity.component';
import { StudentActivityComponent } from './Activity/student-activity/student-activity.component';
import { GroupService } from './_services/group.service';
import { GroupManagementComponent } from './admin/group-management/group-management.component';
import { GroupCardComponent } from './activity/group-card/group-card.component';
import { PostManagementComponent } from './activity/post-management/post-management.component';
import { FollowingComponent } from './following/following.component';
import { InterestManagementComponent } from './admin/interest-management/interest-management.component';
import { InterestComponent } from './admin/interest/interest.component';
import { UserJoinGroupsComponent } from './user-join-groups/user-join-groups.component';
import { ReportManagementComponent } from './admin/report-management/report-management.component';
import { DisabledUserComponent } from './admin/disabled-user/disabled-user.component';
import { BlockUserListComponent } from './admin/block-user-list/block-user-list.component';
import { PendingRequestComponent } from './Activity/pending-request/pending-request.component';
import { PendingGroupsManagementComponent } from './activity/pending-groups-management/pending-groups-management.component';
import { StudentGroupService } from './_services/student-group.service';
import { StudentGroupManagementComponent } from './admin/student-group-management/student-group-management.component';
import { StudentGroupUsersComponent } from './Activity/student-group-users/student-group-users.component';
import { HomeVideoCallingComponent } from './VideoCalling/HomeVideoCalling/HomeVideoCallingComponent';

export function tokenGetter() {
  return localStorage.getItem('token');
}

export class CustomHammerConfig extends HammerGestureConfig  {
  overrides = {
      pinch: { enable: false },
      rotate: { enable: false }
  };
}

@NgModule({
  declarations: [
    TimeAgoPipe,
    AppComponent,
    NavComponent,
    HomeComponent,
    ListsComponent,
    GroupComponent,
    HasRoleDirective,
    RegisterComponent,
    MessagesComponent,
    InterestComponent,
    PostModalComponent,
    GroupCardComponent,
    FollowingComponent,
    MemberListComponent,
    MemberCardComponent,
    GroupModalComponent,
    AdminPanelComponent,
    MemberEditComponent,
    GroupUsersComponent,
    RolesModalComponent,
    PhotoEditorComponent,
    MemberDetailComponent,
    DisabledUserComponent,
    BlockUserListComponent,
    UserJoinGroupsComponent,
    MemberMessagesComponent,
    PostManagementComponent,
    UserManagementComponent,
    PhotoManagementComponent,
    TeacherActivityComponent,
    StudentActivityComponent,
    GroupManagementComponent,
    ReportManagementComponent,
    InterestManagementComponent,
    PendingRequestComponent,
    PendingGroupsManagementComponent,
    StudentGroupManagementComponent,
    StudentGroupComponent,
    StudentGroupPendingRequestComponent,
    StudentGroupUsersComponent,
    UniversityNewsComponent,
    ActivityIndicatorComponent,
    CameraComponent,
    ParticipantsComponent,
    RoomsComponent,
    DeviceSelectComponent,
    SettingsComponent,
    HomeVideoCallingComponent
  ],
  imports: [
    FormsModule,
    BrowserModule,
    FileUploadModule,
    HttpClientModule,
    NgxGalleryModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    TabsModule.forRoot(),
    ModalModule.forRoot(),
    ButtonsModule.forRoot(),
    AccordionModule.forRoot(),
    BsDropdownModule.forRoot(),
    PaginationModule.forRoot(),
    BsDatepickerModule.forRoot(),
    RouterModule.forRoot(appRoutes),
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        whitelistedDomains: ['localhost:5000'],
        blacklistedRoutes: ['localhost:5000/api/auth']
      }
    })
  ],
  providers: [
    AuthGuard,
    UserService,
    PostService,
    AuthService,
    GroupService,
    ListsResolver,
    AlertifyService,
    FollowingService,
    MessagesResolver,
    MemberEditResolver,
    MemberListResolver,
    NotificationService,
    StudentGroupService,
    MemberDetailResolver,
    PreventUnsavedChanges,
    UniversityNewsService,
    PendingRequestService,
    GroupNewsLetterService,
    StudentGroupPostService,
    ErrorInterceptorProvider,
    StudentGroupCommentService,
    DeviceService,
    StorageService,
    VideoChatService,
    StudyGroupPostCommentService,
    StudentGroupNewsLetterService,
    StudentGroupAttendanceService,
    StudentGroupPostCommentService,
    AdminService,
    { provide: HAMMER_GESTURE_CONFIG, useClass: CustomHammerConfig }
    ],
  entryComponents: [
    RolesModalComponent,
    GroupModalComponent,
    PostModalComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
