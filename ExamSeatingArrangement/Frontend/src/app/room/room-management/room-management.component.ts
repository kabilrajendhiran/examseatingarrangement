import {Component, HostListener, OnInit} from '@angular/core';
import {SeatingService} from '../../../services/seating.service';
import {RoomModel} from '../../../shared/models/RoomModel';
import {THIS_EXPR} from '@angular/compiler/src/output/output_ast';
import {NavbarService} from '../../../services/navbar.service';
import {AppToasterService} from '../../../services/app-toaster.service';

@Component({
  selector: 'app-room-management',
  templateUrl: './room-management.component.html',
  styleUrls: ['./room-management.component.css']
})
export class RoomManagementComponent implements OnInit {

  constructor(private seatingService:SeatingService,public nav:NavbarService,public toast:AppToasterService) { }

  innerWidth:any;
  @HostListener('window:resize', ['$event'])
  onResize(event) {
    this.innerWidth = window.innerWidth;

  }

  rooms:RoomModel[]=[];
  addRoomToggle:boolean=false;
  updateRoomOptionsToggle:boolean[]=[];
  updateRoomEditorMode:boolean[]=[];

  updateRoomToggler(index :any)
  {
    this.updateRoomOptionsToggle[index]=!this.updateRoomOptionsToggle[index];
  }

  editorModeToggler(index:any)
  {
    this.updateRoomEditorMode[index]=!this.updateRoomEditorMode[index];
  }

  addRoomToggler()
  {
    this.addRoomToggle=!this.addRoomToggle;
  }

  addRoom(data:any)
  {

    this.seatingService.addRoom(data).subscribe(
      res=>{
      },
      error => {
        this.toast.error("Error",error);
      },
      ()=>{
        this.getRooms();

      }
    );
  }

  getRooms()
  {
    this.seatingService.getRooms().subscribe(res=>{
      this.rooms=res;
    },error => {
      this.toast.error("Error",error);
    });
  }

  updateRoom(data:RoomModel,index:number)
  {
    this.seatingService.updateRoom(data).subscribe(
      res=>{
      },
      error => {
        this.toast.error("Error",error);
      },
      ()=>{

        this.updateRoomToggler(index);
        this.editorModeToggler(index);
        this.getRooms();
      }
    );
  }

  deleteRoom(data:any,index:number)
  {

    this.seatingService.deleteRoom(data).subscribe(
      res=>{

      },
      error => {
        this.toast.error("Error",error);
      },
      ()=>{
        this.getRooms();
        this.updateRoomToggler(index);
      }
    );
  }




  ngOnInit(): void {
    this.getRooms();
    this.onResize(event);
    this.nav.show();
  }

}
