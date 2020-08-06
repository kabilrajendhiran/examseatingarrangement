import { Injectable } from '@angular/core';
import {environment} from '../environments/environment';
import {HttpClient} from '@angular/common/http';
import {UserModel} from '../shared/models/UserModel';
import { JwtHelperService } from '@auth0/angular-jwt';
import {Router} from '@angular/router';
import {BehaviorSubject} from 'rxjs';
import {userParams} from '../shared/models/userParams';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl:string=environment.appUrl;

  profilePic=new BehaviorSubject<string>("assets/img/avatar.png");

   userparams:userParams={
    "username":"Username",
    "role":"user"
  }

  currentUserData=new BehaviorSubject<userParams>(this.userparams);

  currentProfilePic=this.profilePic.asObservable();



  constructor(private http:HttpClient,private router:Router) { }

  changeMemberPhoto(profilePic:string)
  {
      this.profilePic.next(profilePic);
  }

  changeUserData(userparams:userParams)
  {
    this.currentUserData.next(userparams);
  }

  login(data:any)
  {
     return this.http.post<UserModel>(this.baseUrl+'auth/login',data);
  }

  loggedIn():boolean {
    var jwtHelper=new JwtHelperService();

    const tk=localStorage.getItem("token");

    return !jwtHelper.isTokenExpired(tk);
  }

  logout(){
    localStorage.removeItem("imagePath");
    localStorage.removeItem("userName");
    localStorage.removeItem("token");
    this.router.navigate(['']);
    this.changeMemberPhoto("assets/img/avatar.png");
    this.changeUserData(this.userparams);
  }

  getUsers(){
    return this.http.get<UserModel[]>(this.baseUrl+'auth/users');
  }

  deleteUser(id:number)
  {
    return this.http.delete(this.baseUrl+'auth/user/'+id);
  }

  updateUser(user:UserModel)
  {
    return this.http.put(this.baseUrl+'auth/user',user);
  }

  changePassword(data:any)
  {
    return this.http.post(this.baseUrl+'auth/reset',data);
  }

  forgetPassword(data:any)
  {
    return this.http.post(this.baseUrl+'auth/forget',data);
  }


}
