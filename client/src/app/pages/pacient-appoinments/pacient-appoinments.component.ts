import { Component, OnDestroy, OnInit } from '@angular/core';
import { GetAppoinmentDto } from 'app/_models/getAppoinmentDto';
import { gridSettings } from 'app/_models/grid';
import { AppoinmentsService } from 'app/_services/appoinments.service';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-pacient-appoinments',
  templateUrl: './pacient-appoinments.component.html',
  styleUrls: ['./pacient-appoinments.component.css']
})
export class PacientAppoinmentsComponent implements OnDestroy, OnInit {

  dtOptions: DataTables.Settings = {};
  appoinments: GetAppoinmentDto[] = [];

  // We use this trigger because fetching the list of persons can be quite long,
  // thus we ensure the data is fetched before rendering
  dtTrigger: Subject<any> = new Subject<any>();

  constructor(private appoinmentService: AppoinmentsService) { }

  ngOnInit(): void {
    this.initializeGrid();
  }

  initializeGrid() {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 10,
      columnDefs: [
        { orderable: false, targets: 0 },
        { orderable: false, targets: 5 },
      ],
      language: gridSettings,
      order :[]
    };
    this.appoinmentService.getAppoinmentsForPacient().subscribe(data => {
      this.appoinments = data;
      this.dtTrigger.next();
    });
  };

  ngOnDestroy(): void {
    this.dtTrigger.unsubscribe();
  }
}