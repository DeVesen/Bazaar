import { DatePipe } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'dvDateTime'
})
export class DateTimePipe extends 
                DatePipe implements PipeTransform {

  transform(value: any, ...args: any[]): any {
    return super.transform(value, "dd.MM.yyyy HH:mm:ss");
  }

  public static toLocalDateTimeString(date: Date): string {
    if (date) {
      const options = { year: 'numeric', month: '2-digit', day: '2-digit' };
      const localeDate = date
        ? date.toLocaleDateString('de-DE', options)
        : '';
      const localeTime = date
        ? date.toLocaleTimeString()
        : '';
      return `${localeDate} ${localeTime}`
    }
    return '';
  }
}
