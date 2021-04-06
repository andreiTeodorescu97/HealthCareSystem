import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { User } from 'app/_models/user';
import { UserDoctorDto } from 'app/_models/userDoctorDto';
import { AccountService } from 'app/_services/account.service';
import { DoctorService } from 'app/_services/doctor.service';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-doctor-profile',
  templateUrl: './doctor-profile.component.html',
  styleUrls: ['./doctor-profile.component.css']
})
export class DoctorProfileComponent implements OnInit {

  editForm: FormGroup;
  doctor: UserDoctorDto;
  user: User;
  dateFormated: any;

  constructor(private doctorsService: DoctorService, private accountService: AccountService, private toastr: ToastrService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
  }

  ngOnInit(): void {
    this.getDoctor();
    this.initializeForm();
  }

  updateDoctor() {
    console.log(this.editForm.value);
  }

  initializeForm() {
    this.editForm = new FormGroup({
      userName: new FormControl(),
      doctor: new FormGroup({
        firstName: new FormControl('', Validators.required),
        secondName: new FormControl('', Validators.required),
        email: new FormControl('', [Validators.required,Validators.pattern('^[^\\s@]+@[^\\s@]+\\.[^\\s@]{2,}$')]),
        dateOfBirth: new FormControl()
      })
    })
  }

  getDoctor() {
    this.doctorsService.getDoctorByUsername(this.user.userName)
      .subscribe(response => {
        this.doctor = response;
        this.editForm.patchValue(this.doctor, { emitEvent: false });
        this.dateFormated = this.doctor.doctor.dateOfBirth;
        var userdate: any = new Date(this.dateFormated);
        var datePipe = new DatePipe('en-US');
        this.dateFormated = datePipe.transform(userdate, 'dd.MMM.yyyy');
      });
  }

}

