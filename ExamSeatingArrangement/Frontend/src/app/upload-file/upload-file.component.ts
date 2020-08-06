import { Component, OnInit } from '@angular/core';
import {FileUploader} from 'ng2-file-upload';
import {environment} from '../../environments/environment';
import {HttpClient} from '@angular/common/http';
import {NgxSpinnerService} from 'ngx-spinner';
import {NavbarService} from '../../services/navbar.service';
import {AppToasterService} from '../../services/app-toaster.service';
import {ActivatedRoute} from '@angular/router';


@Component({
  selector: 'app-upload-file',
  templateUrl: './upload-file.component.html',
  styleUrls: ['./upload-file.component.css']
})
export class UploadFileComponent implements OnInit {

  selectedFiles:File[] = [];
  appUrl:string=environment.appUrl;


  constructor(private http:HttpClient,private spinner:NgxSpinnerService,public nav:NavbarService,public toast:AppToasterService)
  {}



  onChanges(event){

    var count=event.target.files.length;
    for (var i=0;i<count;i++)
    {

      this.selectedFiles.push(<File>event.target.files[i]);
    }
  }

  onUpload()
  {
    const fd=new FormData();
    for (var j=0;j<this.selectedFiles.length;j++)
    {
        fd.append('files',this.selectedFiles[j]);
    }
    this.spinner.show();
    this.http.post(this.appUrl+'file',fd).subscribe(
      res=>{
        this.spinner.hide();
      },
      error => {
      this.spinner.hide();
      this.toast.error("File upload","File upload failed");

      },
      ()=>{
        this.toast.success("File Upload","File Uploaded Successfully");
      }

    )
  }



  ngOnInit(): void {
      this.nav.show();

  }









}
