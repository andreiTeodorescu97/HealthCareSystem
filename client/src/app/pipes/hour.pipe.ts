import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'hour' })
export class HourPipe implements PipeTransform {
    transform(date: Date) {
      const hours = date.getHours();
      const minutes = date.getMinutes();
      var result;
      if(minutes == 0)
      {
        result =  hours + ':' + minutes + '0';
      }else{
        result =  hours + ':' + minutes;
      }
      return result;
    }
}
