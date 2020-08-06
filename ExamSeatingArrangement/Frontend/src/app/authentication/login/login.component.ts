import {Component, OnInit, ViewChild} from '@angular/core';
import {NgForm} from '@angular/forms';
import {AuthService} from '../../../services/auth.service';
import {UserModel} from '../../../shared/models/UserModel';
import {JwtHelperService} from '@auth0/angular-jwt';
import {NavbarService} from '../../../services/navbar.service';
import {Router} from '@angular/router';
import {AppToasterService} from '../../../services/app-toaster.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

user:UserModel;
  pwd:string='';
  cpwd:string='';
  equalornot:boolean=false;
  formToggle:boolean=true;

  constructor(private auth:AuthService,public nav:NavbarService,private router:Router,public toast:AppToasterService) { }

  formToggler()
  {
    this.formToggle=!this.formToggle;
  }

  login(loginForm:NgForm)
  {
    var jwtService=new JwtHelperService();


    const fd=new FormData();
    fd.append("userName",loginForm.form.value?.userName);
    fd.append("password",loginForm.form.value?.password);

     this.auth.login(fd).subscribe(
       res=>{
         this.user=res;




         localStorage.setItem("imagePath",this.user.profilePic?.fileContents);
         localStorage.setItem("userName",this.user.userName);
         localStorage.setItem("token",this.user.token);
         localStorage.setItem("role",this.user.role);




         this.auth.changeMemberPhoto('data:image/jpg;base64,'+this.user.profilePic?.fileContents);
         this.auth.changeUserData({"username":this.user.userName,"role":this.user.role});


            this.router.navigate(['/dashboard']);
         this.toast.success("Login","Logged in Successfully");

         },
       error => {
         this.toast.error("Login",error);
       }

     );
  }

  forgetPassword(forgetpassword:NgForm)
  {
    const formData=new FormData();
    formData.append("userName",forgetpassword.form.value?.userName);
    formData.append("password",forgetpassword.form.value?.password);



    this.auth.forgetPassword(formData).subscribe(
      res=>{
        this.toast.success("Forget Password","Password changed successfully, please contact your admin to authorize you");
      },error => {
        this.toast.error("Forget password","Error changing password");
      },()=>{
        this.formToggler();
      }
    );
  }

  onkeyPress()
  {
    if ((this.pwd.length>4 && this.cpwd.length>4)&&(this.pwd.length==this.cpwd.length)&&(this.pwd===this.cpwd))
    {
      this.equalornot=true;
    }
    else {this.equalornot=false}

  }


  ngOnInit(): void {
    this.nav.hide();
  }

}
