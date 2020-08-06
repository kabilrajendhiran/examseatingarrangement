export class ExamDetailsModel {
  constructor(
    public id:string,
    public subjectCode:string,
    public subjectName:string,
    public date:string,
    public session:string,
    public count:number
  ) {}

}
