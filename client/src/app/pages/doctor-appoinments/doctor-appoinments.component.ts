import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GetAppoinmentDto } from 'app/_models/getAppoinmentDto';
import { gridSettings } from 'app/_models/grid';
import { AppoinmentsService } from 'app/_services/appoinments.service';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-doctor-appoinments',
  templateUrl: './doctor-appoinments.component.html',
  styleUrls: ['./doctor-appoinments.component.css']
})
export class DoctorAppoinmentsComponent implements OnDestroy, OnInit {

  dtOptions: DataTables.Settings = {};
  appoinments: GetAppoinmentDto[] = [];

  // We use this trigger because fetching the list of persons can be quite long,
  // thus we ensure the data is fetched before rendering
  dtTrigger: Subject<any> = new Subject<any>();

  constructor(private appoinmentService: AppoinmentsService, private router : Router) { }

  ngOnInit(): void {
    this.initializeGrid();
  }

  initializeGrid() {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 10,
      columnDefs: [
        { orderable: false, targets: 0 },
        { orderable: false, targets: 6 },
      ],
      language: gridSettings,
      order :[]
    };
    this.appoinmentService.getAppoinmentsForDoctor().subscribe(data => {
      this.appoinments = data;
      this.dtTrigger.next();
    });
  };

  ngOnDestroy(): void {
    this.dtTrigger.unsubscribe();
  }

  redirectToConsultation(appoinment : GetAppoinmentDto){
    this.router.navigateByUrl('pacient/consultation/' + appoinment.id + "/" + appoinment.pacientFirstName + "/" + appoinment.pacientSecondName);
  }
}