import {Component, HostListener, OnInit, ViewChild} from '@angular/core';
import {SeatingService} from '../../../services/seating.service';
import {RoomModel} from '../../../shared/models/RoomModel';
import {FormGroup} from '@angular/forms';
import {NavbarService} from '../../../services/navbar.service';

@Component({
  selector: 'app-room-dashboard',
  templateUrl: './room-dashboard.component.html',
  styleUrls: ['./room-dashboard.component.css']
})
export class RoomDashboardComponent implements OnInit {
@ViewChild("datesessionform")datesessionForm:FormGroup;

  innerWidth:any;
  @HostListener('window:resize', ['$event'])
  onResize(event) {
    this.innerWidth = window.innerWidth;

  }



  rooms:RoomModel[]=[];
  session:string="";
  dates:string[]=[];
  date:string="";

  constructor(private seatingService:SeatingService,public nav:NavbarService) { }

  onSubmitSession()
  {
    this.session= this.datesessionForm.value.session;

    this.seatingService.getExamDatesOnly(this.session).subscribe(res=>{
      this.dates=res;
    },error => {

    })
  }

  getRooms(date:string,session:string)
  {
    this.seatingService.getRoomsDateAndSessionWise(date,session).subscribe(
      res=>{
        this.rooms=res;
      },error => {

      }
    );
  }


  onSubmitDate()
  {
    this.date=this.datesessionForm.value.examdate;
    this.getRooms(this.date,this.session);
  }

  ngOnInit(): void {
    this.onResize(event);
    this.nav.show();
  }

}
