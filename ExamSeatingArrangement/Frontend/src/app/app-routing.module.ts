import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {UploadFileComponent} from './upload-file/upload-file.component';
import {SeatingPreviewComponent} from './seating/seating-preview/seating-preview.component';
import {ExamsPriorityComponent} from './exam/exams-priority/exams-priority.component';
import {SeatingRoomwisefinalComponent} from './seating/seating-roomwisefinal/seating-roomwisefinal.component';
import {FileSelectorComponent} from './file/file-selector/file-selector.component';

import {NoticeBoardComponent} from './notice-board/notice-board.component';
import {HomeComponent} from './home/home.component';
import {FileManagementComponent} from './file/file-management/file-management.component';
import {ExamManagementComponent} from './exam/exam-management/exam-management.component';
import {RoomDashboardComponent} from './room/room-dashboard/room-dashboard.component';
import {RoomManagementComponent} from './room/room-management/room-management.component';
import {RegisterComponent} from './authentication/register/register.component';
import {LoginComponent} from './authentication/login/login.component';
import {AuthGuard} from './guard/auth.guard';
import {AppDashboardComponent} from './app-dashboard/app-dashboard.component';
import {UserManagementComponent} from './user/user-management/user-management.component';
import {ChangePasswordComponent} from './user/change-password/change-password.component';




const routes: Routes = [
  {path:'',component:HomeComponent},
  {path:'login',component:LoginComponent},
  {path:'register',component:RegisterComponent},
  {
    path:'',
    canActivate:[AuthGuard],
    runGuardsAndResolvers: 'always',
    data: { expectedRole: ['admin','user']},
    children:[
      {path:'upload',component:UploadFileComponent},
      {path:'seatingpreview',component:SeatingPreviewComponent},
      {path:'exams',component:ExamsPriorityComponent},
      {path:'seatingfinal',component:SeatingRoomwisefinalComponent},
      {path:'files',component:FileSelectorComponent},
      {path:'noticeboard',component:NoticeBoardComponent},
      {path:'roomdashboard',component:RoomDashboardComponent},
      {path:'dashboard',component:AppDashboardComponent},
      {path:'changepassword',component: ChangePasswordComponent}
    ]
  },

  {
    path:'',
    canActivate:[AuthGuard],
    runGuardsAndResolvers: 'always',
    data: { expectedRole: ['admin']},
    children:[
      {path:'filemanagement',component:FileManagementComponent},
      {path:'exammanagement',component:ExamManagementComponent},
      {path:'roommanagement',component:RoomManagementComponent},
      {path:'usermanagement',component:UserManagementComponent}

    ]
  },

  {path: '**', redirectTo: '', pathMatch: 'full'},


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
