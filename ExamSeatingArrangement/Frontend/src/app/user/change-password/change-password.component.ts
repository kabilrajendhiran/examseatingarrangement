import { Component, OnInit } from '@angular/core';
import {NgForm} from '@angular/forms';
import {AuthService} from '../../../services/auth.service';
import {AppToasterService} from '../../../services/app-toaster.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {

  pwd:string='';
  cpwd:string='';
  equalornot:boolean=false;

  constructor(private auth:AuthService,public toast:AppToasterService) { }

  ngOnInit(): void {
  }
  onkeyPress()
  {
    if ((this.pwd.length>4 && this.cpwd.length>4)&&(this.pwd.length==this.cpwd.length)&&(this.pwd===this.cpwd))
    {
      this.equalornot=true;
    }
    else {this.equalornot=false}


  }

  onSubmit(changeFormData:NgForm)
  {
    const formData=new FormData();
    formData.append("userName",localStorage.getItem("userName"));
    formData.append("password",changeFormData.form.value?.password);
    this.auth.changePassword(formData).subscribe(
      res=>{
        this.toast.success("Password","Password Changed Successfully!!!");
      },error => {
        this.toast.error("Password","Password change failed ");

      }
    )
  }


}
