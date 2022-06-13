import { UniversityNewsComponent } from './university-news/university-news.component';
import { RegisterComponent } from './register/register.component';
import { StudentGroupUsersComponent } from './Activity/student-group-users/student-group-users.component';
import { StudentGroupPendingRequestComponent } from './Activity/student-group-pending-request/student-group-pending-request.component';
import { StudentGroupComponent } from './Activity/student-group/student-group.component';
import { PendingRequestComponent } from './Activity/pending-request/pending-request.component';
import {Routes} from '@angular/router';
import { AuthGuard } from './_guards/auth.guard';
import { HomeComponent } from './home/home.component';
import { ListsComponent } from './lists/lists.component';
import { ListsResolver } from './_resolvers/lists.resolver';
import { MessagesResolver } from './_resolvers/messages.resolver';
import { GroupComponent } from './Activity/group/group.component';
import { MessagesComponent } from './messages/messages.component';
import { FollowingComponent } from './following/following.component';
import { MemberEditResolver } from './_resolvers/member-edit.resolver';
import { MemberListResolver } from './_resolvers/member-list.resolver';
import { MemberDetailResolver } from './_resolvers/member-detail.resolver';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { GroupUsersComponent } from './Activity/group-users/group-users.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { UserJoinGroupsComponent } from './user-join-groups/user-join-groups.component';
import { BlockUserListComponent } from './admin/block-user-list/block-user-list.component';
import { StudentActivityComponent } from './Activity/student-activity/student-activity.component';
import { TeacherActivityComponent } from './Activity/teacher-activity/teacher-activity.component';
import { PendingGroupsManagementComponent } from './activity/pending-groups-management/pending-groups-management.component';
import { HomeVideoCallingComponent } from './VideoCalling/HomeVideoCalling/HomeVideoCallingComponent';

export const appRoutes: Routes = [
    {path: '', component: HomeComponent},
    {path: 'register', component: RegisterComponent},
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            {path: 'members', component: MemberListComponent,
                resolve: {users: MemberListResolver}},
            {path: 'members/:id', component: MemberDetailComponent,
                resolve: {user: MemberDetailResolver}},
            {path: 'member/edit', component: MemberEditComponent,
                resolve: {user: MemberEditResolver}, canDeactivate: [PreventUnsavedChanges]},
            {path: 'messages', component: MessagesComponent, resolve: {messages: MessagesResolver}},
            {path: 'lists', component: ListsComponent, resolve: {users: ListsResolver}},
            
            {path: 'block/Users/list', component: BlockUserListComponent, data: {roles: ['Admin']}},
            {path: 'admin', component: AdminPanelComponent, data: {roles: ['Admin']}},
            
            {path: 'teacher', component: TeacherActivityComponent, data: {roles: ['Moderator']}},

            {path: 'group/details/:id', component: GroupComponent, data: {roles: ['Member', 'Moderator']}},
            {path: 'student/group/details/:id', component: StudentGroupComponent, data: {roles: ['Member', 'Moderator']}},
            {path: 'student/group/pending-required/:id', component: StudentGroupPendingRequestComponent, data: {roles: ['Member', 'Moderator']}},
            {path: 'group/users/:id', component: GroupUsersComponent, data: {roles: ['Member', 'Moderator']}},
            {path: 'student/group/users/:id', component: StudentGroupUsersComponent, data: {roles: ['Member', 'Moderator']}},
            {path: 'video/calling', component: HomeVideoCallingComponent, data: {roles: ['Member', 'Moderator']}},

            {path: 'student', component: StudentActivityComponent, data: {roles: ['Member']}},
            {path: 'following', component: FollowingComponent, data: {roles: ['Member', 'Moderator']}},
            {path: 'join/groups', component: UserJoinGroupsComponent, data: {roles: ['Member', 'Moderator']}},
            {path: 'pending/requests', component: PendingRequestComponent, data: {roles: ['Member', 'Moderator']}},
            {path: 'university/news', component: UniversityNewsComponent, data: {roles: ['Member', 'Moderator', 'Admin']}},
            {path: 'group/admin/pending/requests', component: PendingGroupsManagementComponent, data: {roles: ['Member', 'Moderator']}},

        ]
    },
    {path: '**', redirectTo: '', pathMatch: 'full'},
];
