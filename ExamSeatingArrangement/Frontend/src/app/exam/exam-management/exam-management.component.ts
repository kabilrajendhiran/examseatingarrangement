import { Component, OnInit } from '@angular/core';
import {ExamModel} from '../../../shared/models/ExamModel';
import {SeatingService} from '../../../services/seating.service';
import {MinExamModel} from '../../../shared/models/MinExamModel';
import {NavbarService} from '../../../services/navbar.service';
import {AppToasterService} from '../../../services/app-toaster.service';

@Component({
  selector: 'app-exam-management',
  templateUrl: './exam-management.component.html',
  styleUrls: ['./exam-management.component.css']
})
export class ExamManagementComponent implements OnInit {

  exams:ExamModel[]=[];
  minexammodel:MinExamModel[]=[];
  minexammodelforupdate:MinExamModel[]=[];

  editorarrayexamwise:boolean[]=[];
  editorarraydatewise:boolean[]=[];
  examViewToggle:boolean=true;
  deleteModeToggle:boolean=false;

  constructor(private seatingService:SeatingService,public nav:NavbarService,public toast:AppToasterService) { }

  getExams()
  {
    this.seatingService.getExams().subscribe(
      data=>{
        this.exams=data;
      },error => {
       this.toast.error("error","Server error");
      }
    );
  }

  toggleEditMode(data:number)
  {
    this.editorarrayexamwise[data]=!this.editorarrayexamwise[data];
  }

  toggleEditModeDatewise(data:number)
  {
    this.editorarraydatewise[data]=!this.editorarraydatewise[data];
  }

  updateExam(data:ExamModel)
  {
    this.seatingService.updateExamDetails(data).subscribe(res=>{

      this.toast.success("Update Exam","Exam details updated successfully");
    },error => {
      this.toast.error("Update Exam","Updating exam details failed");
      }
    );
  }

  updateExamDateWise(data:MinExamModel){
      this.seatingService.updateExamDetailsDateWise(data).subscribe(res=>{

        this.toast.success("Update Exam","Exam details updated successfully");
      },error => {

        this.toast.error("Update Exam","Updating exam details failed");
      });
  }

  getExamDateWise()
  {
    this.seatingService.getExamDates().subscribe(res=>{
      this.minexammodel=res;
    },error => {
      this.toast.error("Error","Server error");
    });
  }

  getExamDatesWithSession()
  {
    this.seatingService.getExamDatesWithSession().subscribe(
      res=>{
        this.minexammodelforupdate=res;
      },error => {
        this.toast.error("Error","Server Error");
      }
    );
  }

  enableExamWise()
  {
    this.examViewToggle=true;
    this.getExams();
  }
  enableDateWise()
  {
     this.examViewToggle=false;
     this.getExamDateWise();
  }

  deleteModeToggler()
  {
    this.deleteModeToggle=!this.deleteModeToggle;
  }

  deleteExam(date:string,session:string)
  {
    this.seatingService.deleteExam(date, session).subscribe(
      res=>{
        this.toast.success("Delete Exam","Exam deleted successfully");
      },error => {
        this.toast.error("Delete Exam","Deleting exam failed, Try again");
      },()=>{
        this.getExamDatesWithSession();
      }
    );
  }

  ngOnInit(): void {
    this.getExams();
    this.nav.show();
    this.getExamDatesWithSession();
  }


}
