import {Component, OnInit, ViewChild} from '@angular/core';
import {SeatingService} from '../../../services/seating.service';
import {FormGroup, NgForm} from '@angular/forms';
import {NavbarService} from '../../../services/navbar.service';
import {AppToasterService} from '../../../services/app-toaster.service';

@Component({
  selector: 'app-seating-roomwisefinal',
  templateUrl: './seating-roomwisefinal.component.html',
  styleUrls: ['./seating-roomwisefinal.component.css']
})
export class SeatingRoomwisefinalComponent implements OnInit {

  @ViewChild("datesessionform")datesessionForm:FormGroup;
  session:string;
  seatingarrangementdata:any
  dates:string[]=[];
  date:string;


  constructor(private seatingService:SeatingService,public nav:NavbarService,public toast:AppToasterService) { }


  onSubmit()
  {

  }

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

  onSubmitDate()
  {
    this.date=this.datesessionForm.value.examdate;
    this.seatingService.seatingFinal(this.date,this.session).subscribe(res=>{this.seatingarrangementdata=res;},
      error => {
      this.toast.error("Seating","Error creating seating final report,try clean and re-create seating preview");

      });
  }


}
