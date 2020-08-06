import {Component, OnInit} from '@angular/core';
import {NgForm} from '@angular/forms';
import {SeatingService} from '../../../services/seating.service';
import {NavbarService} from '../../../services/navbar.service';
import {AppToasterService} from '../../../services/app-toaster.service';
import {Router} from '@angular/router';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  selectedFile:File=null;
  equalornot:boolean=false;
  pwd:string='';
  cpwd:string='';
  selectedFileName:string="";

  constructor(private seatingService:SeatingService,public nav:NavbarService,
              public toast:AppToasterService,private router:Router) { }

  ngOnInit(): void {
    this.nav.hide();
  }

  onSubmit(regformdata:NgForm)
  {
    var formData=new FormData();
    var data=regformdata.form.value;

    if (this.selectedFile!=null) {
      formData.append("profilePicture", this.selectedFile, this.selectedFile.name);
    }

    formData.append("userName",data?.userName);
    formData.append("password",data?.password);

    formData.append("role",data?.role);


    this.seatingService.profileDataUpload(formData).subscribe(
      res=>{

        this.toast.success("Register","Registeration Success!!!");
        this.router.navigate(['/login']);
      },error => {

        this.toast.error("Register","Registeration failed");
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

  onChange(event)
  {
    this.selectedFile=<File>event.target.files[0];
    this.selectedFileName=event.target.files[0].name;

  }

}
