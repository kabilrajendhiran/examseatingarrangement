import { Component, OnInit } from '@angular/core';
import {UserModel} from '../../../shared/models/UserModel';
import {AuthService} from '../../../services/auth.service';
import {NavbarService} from '../../../services/navbar.service';
import {AppToasterService} from '../../../services/app-toaster.service';
import {userParams} from '../../../shared/models/userParams';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.css']
})
export class UserManagementComponent implements OnInit {
users:UserModel[]=[];
editUserMode:boolean[]=[];
userParams:userParams;

  constructor(private authService:AuthService,public nav:NavbarService,public toast:AppToasterService) { }

  getUsers()
  {
      this.authService.getUsers().subscribe(res=>{
        this.users=res;

      },error => {
        this.toast.error("Error",error);
      });
  }

  ngOnInit(): void {
    this.nav.show();
    this.getUsers();
  }

  toggleEditMode(index:number)
  {
    this.editUserMode[index]=!this.editUserMode[index];
  }

  deleteUser(id:number,userName:string)
  {
    this.authService.deleteUser(id).subscribe(res=>{
      this.toast.success("Delete User","User deleted successfully !");
      this.authService.currentUserData.subscribe(
        res=>{
          this.userParams=res;
          if (this.userParams.username==userName)
          {
            this.authService.logout();
          }

        }
      );
    },error => {
      this.toast.error("Delete User","error deleting user");
    },()=>{
      this.getUsers();
    });

  }

  updateUser(user:UserModel)
  {
    this.authService.updateUser(user).subscribe(
      res=>{
        this.toast.success("Update user","User updated successfully!!!");
      },error => {
        this.toast.error("Update user","Updating user failed");

      },()=>{
        this.getUsers();
      }
    );
  }



}
