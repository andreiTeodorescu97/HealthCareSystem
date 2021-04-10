import { Pipe, PipeTransform } from '@angular/core';

enum Months {
  ianuarie,
  februarie,
  martie,
  aprilie,
  mai,
  iunie,
  iulie,
  august,
  septembrie,
  obtombrie,
  noiembrie,
  decembrie
}


@Pipe({ name: 'romanianDate' })
export class RomaniandatePipe implements PipeTransform {
    transform(date: Date) {

        const dayOfMonth = date.getDate();
        const nameOfMonth = Months[date.getMonth()];
        const year = date.getFullYear();

        const result =  dayOfMonth + ' ' + nameOfMonth + ' ' + year;

       return result;
    }
}
