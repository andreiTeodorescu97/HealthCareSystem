import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';

export function NaoDatepickerConfig(): BsDatepickerConfig {
  return Object.assign(new BsDatepickerConfig(), {
/*      dateInputFormat: 'MM/dd/yyyy',   */
dateInputFormat: 'YYYY.MM.DD'
  });
}