import {Component, OnInit, ViewChild} from '@angular/core';
import {SeatingService} from '../../../services/seating.service';
import {SeatingModel} from '../../../shared/models/SeatingModel';
import {FormGroup, NgForm} from '@angular/forms';
import {RoomModel} from '../../../shared/models/RoomModel';
import {NavbarService} from '../../../services/navbar.service';
import {AppToasterService} from '../../../services/app-toaster.service';

@Component({
  selector: 'app-seating-preview',
  templateUrl: './seating-preview.component.html',
  styleUrls: ['./seating-preview.component.css']
})
export class SeatingPreviewComponent implements OnInit {

  @ViewChild("seatform") SeatingForm:NgForm;
  @ViewChild("halls") RoomGroup:FormGroup;
  @ViewChild("datesessionform")datesessionForm:FormGroup;


  dates:string[]=[];
  date:string="";
  session:string="";

  datalist: any[]=[];
  roomseatinglist:Array<any>=[];

  constructor(private seatingService:SeatingService,public nav:NavbarService,public toast:AppToasterService) { }

  ngOnInit(): void {
    this.getRooms();
    this.nav.show();

  }

 roomNumber:RoomModel[]=[];

  seatings:SeatingModel[]=[];


  onSubmit(){

    var roomsdata=this.SeatingForm.value;


    for(let control in this.SeatingForm.form.value)
    {
      var data= this.SeatingForm.form.get(control).value
      this.datalist.push(data);

    }

    this.roomseatinglist=this.datalist.slice(1);




    this.seatingService.display(this.roomseatinglist,this.date,this.session).subscribe(
      res=>{

        this.toast.success("Seating","Seating final report created successfully");
        },
        error => {

          this.toast.error("Error",error);
        this.datalist=[];
        this.roomseatinglist=[];
        this.toast.error("Seating","Error creating Seating final report");

      },()=>{
        this.datalist=[];
        this.roomseatinglist=[];
      }
    );

  }


  getRooms(){
    this.seatingService.getRooms().subscribe(res=>{
      this.roomNumber=res;
    },error => {

      this.toast.error("Rooms","Error failed getting rooms, check the room dashboard")
    })
  }

  getSeatingPreview(date:string,session:string)
  {
      this.seatingService.getSeatingPreview(date, session).subscribe(
        res=>{
          this.seatings=res;
        },error => {

          this.toast.error("Seating Preview Error","Set exam priority in exam-management");

        }
      );
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
      this.getSeatingPreview(this.date,this.session);
  }

}


