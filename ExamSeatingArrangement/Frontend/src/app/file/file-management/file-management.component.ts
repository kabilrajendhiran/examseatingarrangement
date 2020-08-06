import { Component, OnInit } from '@angular/core';
import {SeatingService} from '../../../services/seating.service';
import {FileDetailsModel} from '../../../shared/models/FileDetailsModel';
import {pipe} from 'rxjs';
import {NavbarService} from '../../../services/navbar.service';
import {AppToasterService} from '../../../services/app-toaster.service';

@Component({
  selector: 'app-file-management',
  templateUrl: './file-management.component.html',
  styleUrls: ['./file-management.component.css']
})
export class FileManagementComponent implements OnInit {

  constructor(private seatingService:SeatingService,public nav:NavbarService,public toast:AppToasterService) { }

  pdfSrc:any;
  filedetails:FileDetailsModel[];

  getAllFileDetails(){
    this.seatingService.getAllFileDetails().subscribe(
      data=>{
        this.filedetails=data;
      },
      error => {
       this.toast.error("Error",error);
      }
    );
  }

  getFileForPreview(id:number)
  {
      this.seatingService.getFileForPreview(id).subscribe(data=>{
        this.pdfSrc=data;


      },error => {
        this.toast.error("Error",error);
      });
  }

  deleteFile(id:number)
  {
      this.seatingService.deleteFile(id).subscribe(res=>{

        this.toast.success("File","File Sucessfully Deleted");
      },error => {

        this.toast.error("File","Error Deleting file");
      },()=>{
        this.getAllFileDetails();

      });
  }

  ngOnInit(): void {
    this.getAllFileDetails();
    this.nav.show();
  }

  updateFileStatus(filedetail: FileDetailsModel) {

    this.seatingService.updateStatusOfFileManually(filedetail).subscribe(res=>{

      this.toast.success("File Update","File updated successfully");
    },error => {

      this.toast.error("File update","Error updated successfully");
    });
  }
}
