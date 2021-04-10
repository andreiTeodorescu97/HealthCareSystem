import { Component, OnInit } from '@angular/core';
import { DoctorDto } from 'app/_models/doctorDto';
import { User } from 'app/_models/user';
import { AccountService } from 'app/_services/account.service';
import { DoctorService } from 'app/_services/doctor.service';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-doctors-list',
  templateUrl: './doctors-list.component.html',
  styleUrls: ['./doctors-list.component.css']
})
export class DoctorsListComponent implements OnInit {

  user: User;
  doctors: Array<DoctorDto> = [];

  constructor(private doctorsService: DoctorService, private accountService: AccountService, private toastr: ToastrService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
  }

  ngOnInit(): void {
    this.getDoctors();
  }

  getDoctors(){
    this.doctorsService.getDoctors().subscribe(response => {
      this.doctors = response;
    })
  }

}
