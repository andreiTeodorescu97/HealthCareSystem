import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { DataTableDirective } from 'angular-datatables';
import { gridSettings } from 'app/_models/grid';
import { PacientHistoryDto } from 'app/_models/pacientHistoryDto';
import { DoctorService } from 'app/_services/doctor.service';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-pacient-history',
  templateUrl: './pacient-history.component.html',
  styleUrls: ['./pacient-history.component.css']
})
export class PacientHistoryComponent implements OnInit, OnDestroy {

  pacientsHistory: PacientHistoryDto[] = [];
  dtOptions: DataTables.Settings = {};
  isVisible = false;

  @ViewChild(DataTableDirective, { static: false })
  dtElement: DataTableDirective;

  // We use this trigger because fetching the list of persons can be quite long,
  // thus we ensure the data is fetched before rendering
  dtTrigger: Subject<any> = new Subject<any>();

  constructor(private doctorsService: DoctorService, private router: Router) { }

  ngOnInit(): void {
    this.initializeGrid();
  }

  initializeGrid() {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 25,
      columnDefs: [
        { orderable: false, targets: 5 },
      ],
      language: gridSettings,

    };

    this.doctorsService.getPacientHistory()
      .subscribe(data => {
        this.pacientsHistory = data;
        this.pacientsHistory.forEach(pac => {
          pac.dateOfBirth = new Date(pac.dateOfBirth);
          pac.lastVisitDate = new Date(pac.lastVisitDate);
        });
        this.dtTrigger.next();
      });
  }

  redirectToPacientProfile(pacientId: number) {
    this.router.navigateByUrl('pacient/pacient_profile/' + pacientId);
  }

  ngOnDestroy(): void {
    this.dtTrigger.unsubscribe();
  }
}
