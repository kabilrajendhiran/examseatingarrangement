import { Pipe, PipeTransform } from '@angular/core';
import {DomSanitizer} from '@angular/platform-browser';

@Pipe({
  name: 'imageSanitizer'
})
export class ImageSanitizerPipe implements PipeTransform {

  constructor(private sanitizer:DomSanitizer) {
  }

  transform(html){
    this.sanitizer.bypassSecurityTrustResourceUrl(html);
  }

}
