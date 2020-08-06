import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {AuthService} from '../../services/auth.service';
import {NavbarService} from '../../services/navbar.service';
import {userParams} from '../../shared/models/userParams';
import {AppToasterService} from '../../services/app-toaster.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  imagePath:string="";
  userData:userParams;
  profilePic:string;


  userDataExists:boolean=false;



  constructor(private auth:AuthService,public nav:NavbarService,public toast:AppToasterService,public route:Router) { }


  isCollapsed = false;
  isCollapsedFile=true;
  isCollapsedSeating=true;
  isCollapsedExam=true;
  isCollapsedRoom=true;
  isCollapsedUser=true;

   isloggedin()
   {
     return this.auth.loggedIn();
   }




  ngOnInit(): void {

    this.nav.show();
    this.auth.profilePic.subscribe(profilepic=>this.profilePic=profilepic);
    this.auth.currentUserData.subscribe(userparams=>this.userData=userparams);

  }

  logout()
  {
      this.auth.logout();
      this.toast.info("Logout","You are logged out successfully");
  }






}
