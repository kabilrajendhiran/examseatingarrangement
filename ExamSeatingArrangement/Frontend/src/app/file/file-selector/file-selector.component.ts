import { Component, OnInit } from '@angular/core';
import {SeatingService} from '../../../services/seating.service';
import {FileDetailsModel} from '../../../shared/models/FileDetailsModel';
import {NgForm} from '@angular/forms';
import {NavbarService} from '../../../services/navbar.service';
import {AppToasterService} from '../../../services/app-toaster.service';

@Component({
  selector: 'app-file-selector',
  templateUrl: './file-selector.component.html',
  styleUrls: ['./file-selector.component.css']
})
export class FileSelectorComponent implements OnInit {
filedetails:any;
readpdfresponse:any;
info:string="Hover on the controls to see the info";

  constructor(private seatingService:SeatingService,public nav:NavbarService,public toast:AppToasterService) { }

  getAllFileDetails(){
    this.seatingService.getAllFileDetails().subscribe(
      res=>{
        this.filedetails=res;

      },error => {
       this.toast.error("Error",error);

      }
    );
  }

  onSubmit(data:NgForm){

    this.seatingService.readPdf(data.form.value).subscribe(res=>{
      this.readpdfresponse=res;
      this.toast.success("File","Seating Preview created successfully")

    },error => {
      this.toast.error("File","Error Occured");

    });
  }

  ngOnInit(): void {
    this.getAllFileDetails();
    this.nav.show();
  }

  onhoverFileControl(){
      this.info="Select the uploaded attendance sheet pdf.";
  }

  onHoverDate()
  {
    this.info="This date should be same as the exam date";
  }

  onHoverSemesterMonth()
  {
    this.info="Enter the Abbrevation of Months that the semester going to held. Eg: Apr/May"
  }

}
