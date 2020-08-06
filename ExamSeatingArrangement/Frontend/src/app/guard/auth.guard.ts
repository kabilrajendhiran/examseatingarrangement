import { Injectable } from '@angular/core';
import {CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router} from '@angular/router';
import { Observable } from 'rxjs';
import {AuthService} from '../../services/auth.service';
import {AppToasterService} from '../../services/app-toaster.service';


@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private auth: AuthService,private router:Router,public toast:AppToasterService) {
  }

  canActivate(route: ActivatedRouteSnapshot): boolean {

    let expectedRoleArray = route.data;
    expectedRoleArray = expectedRoleArray.expectedRole;
    const role=localStorage.getItem("role");

    let  expectedRole = '';

    for(let i=0; i<expectedRoleArray.length; i++){
      if(expectedRoleArray[i]==role){

        expectedRole = role;
      }
    }



    if (this.auth.loggedIn()&& role==expectedRole)
    {
      return true;
    }
    this.toast.warning("Malicious Activity","You are not authorized to visit that page");
    this.router.navigate(['/login']);
    return false;
  }

}
