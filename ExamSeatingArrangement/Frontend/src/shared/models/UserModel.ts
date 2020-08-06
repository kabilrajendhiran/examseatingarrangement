export class UserModel {
  constructor(
   public id:number,
   public userName:string,
   public role:string,
   public profilePic:any,
   public token:string,
   public isAuthorized:boolean
  ) {
  }

}
