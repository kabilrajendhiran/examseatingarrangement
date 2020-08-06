import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {HttpClientModule} from '@angular/common/http';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { UploadFileComponent } from './upload-file/upload-file.component';
import {SeatingService} from '../services/seating.service';
import {FileUploadModule} from 'ng2-file-upload';
import {FormsModule} from '@angular/forms';
import { SeatingPreviewComponent } from './seating/seating-preview/seating-preview.component';
import { SeatingComponent } from './seating/seating.component';
import { ExamsPriorityComponent } from './exam/exams-priority/exams-priority.component';
import { SeatingRoomwisefinalComponent } from './seating/seating-roomwisefinal/seating-roomwisefinal.component';
import { FileManagementComponent } from './file/file-management/file-management.component';
import { FileSelectorComponent } from './file/file-selector/file-selector.component';

import { DpDatePickerModule } from "ng2-date-picker";
import { NgxSpinnerModule } from 'ngx-spinner';
import {NgxPrintModule} from 'ngx-print';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { NgxNavbarModule } from 'ngx-bootstrap-navbar';
import {CollapseModule} from 'ngx-bootstrap/collapse';
import { NoticeBoardComponent } from './notice-board/notice-board.component';
import {HomeComponent} from './home/home.component';
import {NgxExtendedPdfViewerModule} from 'ngx-extended-pdf-viewer';
import { ExamManagementComponent } from './exam/exam-management/exam-management.component';
import { RoomDashboardComponent } from './room/room-dashboard/room-dashboard.component';
import { RoomManagementComponent } from './room/room-management/room-management.component';
import { RegisterComponent } from './authentication/register/register.component';
import { LoginComponent } from './authentication/login/login.component';
import {AuthService} from '../services/auth.service';
import {AuthGuard} from './guard/auth.guard';
import {NavbarService} from '../services/navbar.service';
import {ToasterModule} from 'angular2-toaster';
import {AppToasterService} from '../services/app-toaster.service';
import { AppDashboardComponent } from './app-dashboard/app-dashboard.component';
import {JwtModule} from '@auth0/angular-jwt';
import { UserManagementComponent } from './user/user-management/user-management.component';
import { ImageSanitizerPipe } from './helpers/pipes/image-sanitizer.pipe';
import { ChangePasswordComponent } from './user/change-password/change-password.component';

import {ErrorInterceptorProvider} from '../services/error-interceptor.service';


export function tokenGetter() {
  return localStorage.getItem("token");
}



@NgModule({
  declarations: [
    AppComponent,
    UploadFileComponent,
    SeatingPreviewComponent,
    SeatingComponent,
    ExamsPriorityComponent,
    SeatingRoomwisefinalComponent,
    FileManagementComponent,
    FileSelectorComponent,
    NavbarComponent,
    NoticeBoardComponent,
    HomeComponent,
    ExamManagementComponent,
    RoomDashboardComponent,
    RoomManagementComponent,
    RegisterComponent,
    LoginComponent,
    AppDashboardComponent,
    UserManagementComponent,
    ImageSanitizerPipe,
    ChangePasswordComponent,

  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    HttpClientModule,
    FileUploadModule,
    FormsModule,
    DpDatePickerModule,
    NgxSpinnerModule,
    NgxPrintModule,
    BsDropdownModule,
    NgxNavbarModule,
    CollapseModule,
    NgxExtendedPdfViewerModule,
    ToasterModule.forRoot(),
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        whitelistedDomains: ['localhost:5000'],
        blacklistedRoutes: ['localhost:5000/api/auth']
      }
    })

  ],
  providers: [ErrorInterceptorProvider,SeatingService,AuthGuard,AuthService,NavbarService,AppToasterService],
  bootstrap: [AppComponent]
})
export class AppModule { }
