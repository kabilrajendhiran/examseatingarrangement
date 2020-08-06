import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../environments/environment';
import {Form, NgForm} from '@angular/forms';
import {AllocationSeatingModel} from '../shared/models/AllocationSeatingModel';
import {DepartmentModel} from '../shared/models/DepartmentModel';
import {ExamDetailsModel} from '../shared/models/ExamDetailsModel';
import {SeatingModel} from '../shared/models/SeatingModel';
import {Observable} from 'rxjs';
import {FileDetailsModel} from '../shared/models/FileDetailsModel';
import {ExamModel} from '../shared/models/ExamModel';
import {MinExamModel} from '../shared/models/MinExamModel';
import {RoomModel} from '../shared/models/RoomModel';

@Injectable({
  providedIn: 'root'
})
export class SeatingService {

  constructor(private http:HttpClient) { }
  baseUrl:string=environment.appUrl;


  uploadFile(filedata:FormData){
   return this.http.post(this.baseUrl+'uploadfile',filedata);
  }

  getSeatingDetails(){
    return this.http.get<AllocationSeatingModel[]>(this.baseUrl+'seating/forallocation');
  }

  getDepartments(date:string,session:string){
    return this.http.get<DepartmentModel[]>(this.baseUrl+'seating/departments/'+date+'/'+session);
  }

  getExams(){
    return this.http.get<ExamModel[]>(this.baseUrl+'seating/examdetails');
  }

  getExamDetails(date:string,session:string){
    return this.http.get<ExamDetailsModel[]>(this.baseUrl+'seating/examdetails/'+date+'/'+session)
  }

  getExamDatesOnly(session:string){
    return this.http.get<string[]>(this.baseUrl+'seating/examdates/'+session);
  }

  getExamDates(){
    return this.http.get<MinExamModel[]>(this.baseUrl+'seating/examdates');
  }

  getExamDatesWithSession(){
    return this.http.get<MinExamModel[]>(this.baseUrl+'seating/examdateswithsession');
  }

  deleteExam(date:string,session:string)
  {
    return this.http.delete(this.baseUrl+"seating/exam/"+date+"/"+session);
  }

  setOrder(data:NgForm)
  {
    return this.http.post(this.baseUrl+'seating/examorder',data.value);
  }

  getSeatingPreview(date:string,session:string)
  {
    return this.http.get<SeatingModel[]>(this.baseUrl+'seating/seatingpreviewnew/'+date+'/'+session);
  }

  getRooms(){
    return this.http.get<RoomModel[]>(this.baseUrl+'seating/rooms');
  }

  addRoom(roomName:any){
     return this.http.post(this.baseUrl+'seating/room/'+roomName,{});
  }

  updateRoom(roomName:RoomModel){
    return this.http.put(this.baseUrl+'seating/room',roomName)
  }

  deleteRoom(roomName:any){
    return this.http.delete(this.baseUrl+'seating/room/'+roomName);
  }

  getRoomsDateAndSessionWise(date:string,session:string){
     return this.http.get<RoomModel[]>(this.baseUrl+'seating/rooms/'+date+'/'+session);
  }

  display(halls:any,date:string,session:string){
    return this.http.post(this.baseUrl+'seating/display/'+date+'/'+session , halls);
  }

  seatingFinal(date:string,session:string)
  {
    return this.http.get(this.baseUrl+'seating/finalseating/'+date+'/'+session);
  }

  getAllFileDetails()
  {
    return this.http.get<FileDetailsModel[]>(this.baseUrl+'file/getallfiledetails');
  }

  readPdf(details:any)
  {
    return this.http.post(this.baseUrl+'file/readpdf/',details);
  }

  getNoticeBoard(date:string, session:string)
  {
     return this.http.get(this.baseUrl+'seating/noticeboard/'+date+'/'+session);
  }

  getFileForPreview(id:number)
  {
     return this.http.get(this.baseUrl+'file/'+id);
  }

  deleteFile(id:number)
  {
    return this.http.delete(this.baseUrl+'file/'+id);
  }

  updateStatusOfFileManually(filedata:FileDetailsModel)
  {
    return this.http.put(this.baseUrl+'file',filedata);
  }

  updateExamDetails(examData:ExamModel)
  {
    return this.http.put(this.baseUrl+'seating/updateexamdetails',examData);
  }

  updateExamDetailsDateWise(examData:MinExamModel)
  {
    return this.http.put(this.baseUrl+'seating/updateexamsdetails',examData);
  }

  profileDataUpload(formData:FormData)
  {
     return this.http.post(this.baseUrl+'auth/register',formData);
  }
}
