import { Pipe, PipeTransform } from '@angular/core';
import { IfStmt } from '@angular/compiler';

@Pipe({
  name: 'MessageDate'
})
export class DatePipe implements PipeTransform {

  transform(value: Date): string {
    let messageTime=new Date(value);
    let today=new Date();
    if(messageTime.getDay()==today.getDay()){
      return `${messageTime.getHours()} :${messageTime.getMinutes()}`
    }

  }

}
