import {Component, OnInit, ViewChild} from '@angular/core';
import {SeatingService} from '../../services/seating.service';
import {FormGroup} from '@angular/forms';
import {NavbarService} from '../../services/navbar.service';
import {AppToasterService} from '../../services/app-toaster.service';

@Component({
  selector: 'app-notice-board',
  templateUrl: './notice-board.component.html',
  styleUrls: ['./notice-board.component.css']
})
export class NoticeBoardComponent implements OnInit {

  @ViewChild("datesessionform")datesessionForm:FormGroup;
  session:string;
  dates:string[]=[];
  date:string;
  noticeboard:any;

  constructor(private seatingService:SeatingService,public nav:NavbarService,public toast:AppToasterService) { }

  ngOnInit(): void {
    this.nav.show();
  }

  onSubmitSession()
  {
    this.session= this.datesessionForm.value.session;

    this.seatingService.getExamDatesOnly(this.session).subscribe(res=>{
      this.dates=res;
    },error => {
      this.toast.error("Error",error);
    })
  }


  onSubmitDate() {

    this.date=this.datesessionForm.value.examdate;
   this.seatingService.getNoticeBoard(this.date,this.session).subscribe(
     res=>{
       this.noticeboard=res;
     },
     error => {

       this.toast.error("Report","Error creating report,try clean and re-create seating final");
     }
   );
  }
}
