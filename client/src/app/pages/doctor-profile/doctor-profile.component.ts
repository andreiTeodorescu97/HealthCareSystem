import { DatePipe } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormArray, FormControl, FormGroup, FormGroupDirective, Validators } from '@angular/forms';
import { User } from 'app/_models/user';
import { UserDoctorDto } from 'app/_models/userDoctorDto';
import { StudiesAndExperience } from 'app/_models/studiesAndExperienceDto';
import { AccountService } from 'app/_services/account.service';
import { DoctorService } from 'app/_services/doctor.service';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { BsDatepickerConfig, BsLocaleService } from 'ngx-bootstrap/datepicker';

@Component({
  selector: 'app-doctor-profile',
  templateUrl: './doctor-profile.component.html',
  styleUrls: ['./doctor-profile.component.css']
})
export class DoctorProfileComponent implements OnInit {

  editForm: FormGroup;
  doctor: UserDoctorDto;
  user: User;
  studiesArray: Array<StudiesAndExperience> = [];
  newStudy: StudiesAndExperience = { name: '', location: '', startDate: null, endDate: null };
  locale = 'ro';
  bsConfig: Partial<BsDatepickerConfig>;

  constructor(private doctorsService: DoctorService, private accountService: AccountService, private toastr: ToastrService,
    private localeService: BsLocaleService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
    this.bsConfig = {
      containerClass: 'theme-green',
      dateInputFormat: 'DD MMMM YYYY',
    }
    this.localeService.use(this.locale);
  }

  ngOnInit(): void {
    this.initializeForm();
    this.getDoctor();
  }

  initializeForm() {
    this.editForm = new FormGroup({
      userName: new FormControl(),
      doctor: new FormGroup({
        firstName: new FormControl('', Validators.required),
        secondName: new FormControl('', Validators.required),
        email: new FormControl('', [Validators.required, Validators.pattern('^[^\\s@]+@[^\\s@]+\\.[^\\s@]{2,}$')]),
        dateOfBirth: new FormControl(),
      })
    })
  }

  updateDoctor() {
    const user: UserDoctorDto = this.editForm.value;
    user.doctor.studiesAndExperience = this.studiesArray;
    console.log(user);
    this.doctorsService.updateDoctor(user).subscribe(() => {
      this.toastr.success(
        '<span data-notify="icon" class="nc-icon nc-bell-55"></span><span data-notify="message">Profilul a fost actualizat cu succes!</span>',
        "Update",
        {
          toastClass: "alert alert-success alert-with-icon",
        }
      );
      this.accountService.logout();
    })
  }

  getDoctor() {
    this.doctorsService.getDoctorByUsername(this.user.userName)
      .subscribe(response => {
        this.doctor = response;
        this.editForm.patchValue(this.doctor, { emitEvent: false });
        this.doctor.doctor.studiesAndExperience.forEach(study => {
          this.studiesArray.push(study);
        });
      });
  }

  addFieldValue() {
    this.studiesArray.push(this.newStudy)
    this.newStudy = {name: '', location: '', startDate: null, endDate: null};
  }

  deleteFieldValue(index) {
    this.studiesArray.splice(index, 1);
  }

}

