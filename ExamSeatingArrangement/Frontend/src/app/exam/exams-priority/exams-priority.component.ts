import {Component, OnInit, ViewChild} from '@angular/core';
import {SeatingService} from '../../../services/seating.service';
import {ExamDetailsModel} from '../../../shared/models/ExamDetailsModel';
import {NgForm} from '@angular/forms';
import {DepartmentModel} from '../../../shared/models/DepartmentModel';
import {NavbarService} from '../../../services/navbar.service';
import {AppToasterService} from '../../../services/app-toaster.service';

@Component({
  selector: 'app-exams-priority',
  templateUrl: './exams-priority.component.html',
  styleUrls: ['./exams-priority.component.css']
})
export class ExamsPriorityComponent implements OnInit {

  @ViewChild("dateForm") dateForm:NgForm;
  @ViewChild("sessionForm") sessionForm:NgForm;
  @ViewChild("priorityForm") priorityForm:NgForm;

  dates:string[]=[];
  date:string="";
  exams:ExamDetailsModel[]=[];
 // order:number[]=[];
  session:string;


  constructor(private seatingService:SeatingService,public nav:NavbarService,public toast:AppToasterService) { }




  ngOnInit(): void {
      this.nav.show();
  }


  onSubmitDate()
  {

  this.date=this.dateForm.value.examdate;
    this.session=this.sessionForm.value.session;

    this.seatingService.getExamDetails(this.date,this.session)
      .subscribe(

        res=>{this.exams=res

        },
        error => {
          this.toast.error("Error",error);
        })
  }

  onSubmitSession()
  {
    this.session=this.sessionForm.value.session;

    this.seatingService.getExamDatesOnly(this.session)
      .subscribe(
        res=>{this.dates=res},
          error =>{
          this.toast.error("Error",error);
          },
          );

  }

  onSubmit(){

  this.seatingService.setOrder(this.priorityForm).subscribe(res=>{
  this.toast.success("Exam Priority","Exam priorities updated");
  },error => {
    this.toast.error("Exam Priority","Updating exam priority failed");

  });
  }

}
