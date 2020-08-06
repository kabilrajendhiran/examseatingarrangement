import {Component, OnInit} from '@angular/core';
import {
  Router,
  // import as RouterEvent to avoid confusion with the DOM Event
  Event as RouterEvent,
  NavigationStart,
  NavigationEnd,
  NavigationCancel,
  NavigationError
} from '@angular/router'


import {NgxSpinnerService} from 'ngx-spinner';
import {AuthService} from '../services/auth.service';
import {NavbarService} from '../services/navbar.service';
import {AppToasterService} from '../services/app-toaster.service';
import {ToasterConfig} from 'angular2-toaster';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'SeatingArrangement2020SPA';
  profilePic:string;





 constructor(

   private spinner:NgxSpinnerService,
   private router:Router,
   private auth:AuthService,
   public toast:AppToasterService,
   public nav:NavbarService   ) {

   this.router.events.subscribe((e:RouterEvent)=>{this.navigationInterveptor(e);});

 }



 navigationInterveptor(event: RouterEvent){

   if (event instanceof NavigationStart) {
    this.spinner.show();
   }
   if (event instanceof NavigationEnd) {
     this.spinner.hide();
   }

   // Set loading state to false in both of the below events to hide the spinner in case a request fails
   if (event instanceof NavigationCancel) {
    this.spinner.hide();
   }
   if (event instanceof NavigationError) {
    this.spinner.hide();
   }

 }






 ngOnInit(): void {
    const token:string=localStorage.getItem("token");


    if (token)
    {
      this.profilePic=localStorage.getItem("imagePath");
      this.profilePic='data:image/jpg;base64,'+this.profilePic;
      this.auth.changeMemberPhoto(this.profilePic);
      const role:string=localStorage.getItem("role")
      const username:string=localStorage.getItem("userName");
      this.auth.changeUserData({"username":username,"role":role});

    }

  }




}
