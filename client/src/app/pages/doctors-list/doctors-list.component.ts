import { HttpClient } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DoctorGriDto } from 'app/_models/doctorGridDto';
import { gridSettings } from 'app/_models/grid';
import { User } from 'app/_models/user';
import { AccountService } from 'app/_services/account.service';
import { DoctorService } from 'app/_services/doctor.service';
import { ToastrService } from 'ngx-toastr';
import { Subject } from 'rxjs';
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-doctors-list',
  templateUrl: './doctors-list.component.html',
  styleUrls: ['./doctors-list.component.css']
})
export class DoctorsListComponent implements OnDestroy, OnInit {

  user: User;
  doctors: DoctorGriDto[] = [];
  dtOptions: DataTables.Settings = {};
  isVisible = false;

  // We use this trigger because fetching the list of persons can be quite long,
  // thus we ensure the data is fetched before rendering
  dtTrigger: Subject<any> = new Subject<any>();

  constructor(private doctorsService: DoctorService, private accountService: AccountService, 
    private toastr: ToastrService, private httpClient: HttpClient, private router: Router) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
  }

  ngOnInit(): void {
    this.initializeGrid();
  }

  initializeGrid() {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 10,
      columnDefs: [
        { orderable: false, targets: 5 },
      ],
      language: gridSettings,
      rowCallback: (row: Node, data: any[] | Object, index: number) => {
        const self = this;

        $('td', row).off('click');
        $('td', row).on('click', () => {
          self.someClickHandler(data);
        });
        return row;
      }
    };
    this.doctorsService.getDoctors()
      .subscribe(data => {
        this.doctors = data;
        this.doctors.forEach(doctor => {
          doctor.dateOfBirth = new Date(doctor.dateOfBirth);
        });
        this.dtTrigger.next();
      });
  }

  someClickHandler(info: any): void {
    this.router.navigateByUrl('doctor/detail/' + info[1]);
    console.log(info);
  }

  ngOnDestroy(): void {
    this.dtTrigger.unsubscribe();
  }
}
