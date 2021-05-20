import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { DataTableDirective } from 'angular-datatables';
import { GetAppoinmentDto } from 'app/_models/getAppoinmentDto';
import { gridSettings } from 'app/_models/grid';
import { UpdateAppoinmentStatusDto } from 'app/_models/updateAppoinmentStatusDto';
import { AppoinmentsService } from 'app/_services/appoinments.service';
import { AppoinmentsStatuses } from 'Constants';
import { ToastrService } from 'ngx-toastr';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-pacient-appoinments',
  templateUrl: './pacient-appoinments.component.html',
  styleUrls: ['./pacient-appoinments.component.css']
})
export class PacientAppoinmentsComponent implements OnDestroy, OnInit {

  dtOptions: DataTables.Settings = {};
  appoinments: GetAppoinmentDto[] = [];
  statuses = AppoinmentsStatuses;
  
  @ViewChild(DataTableDirective, {static: false})
  dtElement: DataTableDirective;
  // We use this trigger because fetching the list of persons can be quite long,
  // thus we ensure the data is fetched before rendering
  dtTrigger: Subject<any> = new Subject<any>();

  constructor(private appoinmentService: AppoinmentsService, private toastr: ToastrService) { }

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

  updateStatus(newStatusId: number, appoinmentId : number){
    const updateStatusModel: UpdateAppoinmentStatusDto = {
      appoinmentId: appoinmentId,
      newStatusId: newStatusId,
    };

    this.appoinmentService.updateAppoinmentStatus(updateStatusModel).subscribe(() => {
      this.toastr.success(
        '<span data-notify="icon" class="nc-icon nc-bell-55"></span><span data-notify="message">Statusul a fost actualizat cu succes!</span>',
        "Status",
        {
          toastClass: "alert alert-success alert-with-icon",
        }
      );
      this.rerender();
    })
  }

  rerender(): void {
    this.dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
      // Destroy the table first
      dtInstance.destroy();
      // Call the dtTrigger to rerender again
      this.appoinmentService.getAppoinmentsForPacient().subscribe(data => {
        this.appoinments = data;
        this.dtTrigger.next();
      });
    });
  }

  ngOnDestroy(): void {
    this.dtTrigger.unsubscribe();
  }
}