import { HttpClient } from '@angular/common/http';
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { DataTableDirective } from 'angular-datatables';
import { DoctorGriDto } from 'app/_models/doctorGridDto';
import { gridSettings } from 'app/_models/grid';
import { User } from 'app/_models/user';
import { DoctorFilterDto } from 'app/_models/_filters/doctorFilterDto';
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

  @ViewChild(DataTableDirective, {static: false})
  dtElement: DataTableDirective;

  // We use this trigger because fetching the list of persons can be quite long,
  // thus we ensure the data is fetched before rendering
  dtTrigger: Subject<any> = new Subject<any>();

  filterDoctor = {} as DoctorFilterDto;

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
      searching: false,
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
    this.initializeFilterObject();
    this.doctorsService.getDoctors(this.filterDoctor)
      .subscribe(data => {
        this.doctors = data;
        this.doctors.forEach(doctor => {
          doctor.dateOfBirth = new Date(doctor.dateOfBirth);
        });
        this.dtTrigger.next();
      });
  }

  rerender(): void {
    this.dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
      // Destroy the table first
      dtInstance.destroy();
      // Call the dtTrigger to rerender again
      this.filterDoctor.id = this.filterDoctor.id ?? 0;
      this.filterDoctor.age = this.filterDoctor.age ?? 0;
      this.doctorsService.getDoctors(this.filterDoctor).subscribe(data => {
        this.doctors = data;
        this.doctors.forEach(doctor => {
          doctor.dateOfBirth = new Date(doctor.dateOfBirth);
        });
        this.dtTrigger.next();
      });
    });
  }

  someClickHandler(info: any): void {
    this.router.navigateByUrl('doctor/detail/' + info[1]);
  }

  ngOnDestroy(): void {
    this.dtTrigger.unsubscribe();
  }

  filterDoctorList(){
    console.log(this.filterDoctor);
    this.rerender();
  }

  resetFilter(){
    this.initializeFilterObject();
    this.rerender();
  }

  initializeFilterObject(){
/*     this.filterDoctor = 
    {id: 0, firstName: "", secondName: "", email : "", age: 0 } as DoctorFilterDto; */
    this.filterDoctor = {} as DoctorFilterDto;
  }
}
