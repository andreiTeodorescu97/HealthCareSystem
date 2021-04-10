import { Component, OnInit } from '@angular/core';
import { User } from 'app/_models/user';
import { WorkDayDto } from 'app/_models/workDayDto';
import { AccountService } from 'app/_services/account.service';
import { DoctorService } from 'app/_services/doctor.service';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-doctor-work-days',
  templateUrl: './doctor-work-days.component.html',
  styleUrls: ['./doctor-work-days.component.css']
})
export class DoctorWorkDaysComponent implements OnInit {

  user: User;
  workDaysArray: Array<WorkDayDto> = [];
  newWorkDay: WorkDayDto = { day: null, startHour: null, endHour: null };
  days = [{ name: 'Luni' }, { name: 'Marti' }, { name: 'Miercuri' }, { name: 'Joi' }, { name: 'Vineri' }, { name: 'Sambata' }, { name: 'Duminica' }];

  constructor(private doctorsService: DoctorService, private accountService: AccountService, private toastr: ToastrService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
  }

  ngOnInit(): void {
    this.getWorkDays();
  }

  addFieldValue() {
    this.workDaysArray.push(this.newWorkDay)
    this.newWorkDay = { day: null, startHour: null, endHour: null };
  }

  deleteFieldValue(index) {
    this.workDaysArray.splice(index, 1);
  }

  validateInputs(): boolean {
    var keepGoing = true;
    this.workDaysArray.forEach(workDay => {
      if (keepGoing) {
        if (workDay.startHour == null || workDay.endHour == null || workDay.day == null) {
          this.toastr.error(
            '<span data-notify="icon" class="nc-icon nc-bell-55"></span><span data-notify="message">Va rugam completati toate campurile sau verificati corectitudinea datelor!</span>',
            'Date invalide!',
            {
              toastClass: "alert alert-danger alert-with-icon",
            }
          );
          keepGoing = false;
        }
        const count = this.workDaysArray.filter((obj) => obj.day === workDay.day).length;

        if (count > 1) {
          this.toastr.error(
            '<span data-notify="icon" class="nc-icon nc-bell-55"></span><span data-notify="message">Nu puteti adauga aceeasi zi de doua ori!</span>',
            'Date invalide!',
            {
              toastClass: "alert alert-danger alert-with-icon",
            }
          );
          keepGoing = false;
        }

        if(workDay.startHour > workDay.endHour){
          this.toastr.error(
            '<span data-notify="icon" class="nc-icon nc-bell-55"></span><span data-notify="message">Ora de venire nu poate fi mai mare ca ora de plecare!</span>',
            'Date invalide!',
            {
              toastClass: "alert alert-danger alert-with-icon",
            }
          );
          keepGoing = false;
        }
      }
    }
    )
    if (keepGoing) {
      return true;
    } else {
      return false;
    }
  }

  getWorkDays(){
    this.doctorsService.getWorkDays().subscribe(response => {
      this.workDaysArray = response;
    })
  };

  save() {
    if (this.validateInputs() == true) {
      console.log(this.workDaysArray);
      this.doctorsService.updateWorkDays(this.workDaysArray).subscribe(() => {      
        this.toastr.success(
        '<span data-notify="icon" class="nc-icon nc-bell-55"></span><span data-notify="message">Profilul a fost actualizat cu succes!</span>',
        "Update",
        {
          toastClass: "alert alert-success alert-with-icon",
        }
      );

      })
    }
  }

}
