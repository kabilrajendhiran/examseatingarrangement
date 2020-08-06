import { Injectable } from '@angular/core';
import {ToasterConfig,ToasterService} from 'angular2-toaster';

@Injectable({
  providedIn: 'root'
})
export class AppToasterService {

  constructor(private toaster:ToasterService) { }

  public config: ToasterConfig =
    new ToasterConfig({
      showCloseButton: false,
      tapToDismiss: false,
      timeout: 4000,
      positionClass: 'toast-bottom-right',
      animation: 'fade'
    });


  showToast(type:string,title:string,body:string)
  {
    this.toaster.pop(type, title, body);
  }

  success(title:string,body:string)
  {
    this.toaster.pop("success",title,body);
  }
  error(title:string,body:string)
  {
    this.toaster.pop("error",title,body);
  }
  warning(title:string,body:string)
  {
    this.toaster.pop("warning",title,body);
  }
  info(title:string,body:string)
  {
    this.toaster.pop("info",title,body);
  }



}
