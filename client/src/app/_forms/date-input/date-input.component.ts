import { Component, Input, OnInit, Self } from '@angular/core';
import { NgControl } from '@angular/forms';
import { BsDatepickerConfig, BsLocaleService } from 'ngx-bootstrap/datepicker';

@Component({
  selector: 'app-date-input',
  templateUrl: './date-input.component.html',
  styleUrls: ['./date-input.component.css']
})
export class DateInputComponent implements OnInit {

  @Input() label : string;
  @Input() maxDate: Date;
  @Input() minDate: Date;
  locale = 'ro';
  
  //we can provide only some of the configuration option from bsdatepicker
  bsConfig: Partial<BsDatepickerConfig>;

  constructor(@Self() public ngControl : NgControl, private localeService: BsLocaleService) {
    this.ngControl.valueAccessor = this;
    this.bsConfig = {
      containerClass: 'theme-dark-blue',
      dateInputFormat: 'DD MMMM YYYY',
      isAnimated: true,
      adaptivePosition: true
    }
    this.localeService.use(this.locale);
   }

  writeValue(obj: any): void {
  }
  registerOnChange(fn: any): void {
      }
  registerOnTouched(fn: any): void {
     }

  ngOnInit(): void {
  }
}
