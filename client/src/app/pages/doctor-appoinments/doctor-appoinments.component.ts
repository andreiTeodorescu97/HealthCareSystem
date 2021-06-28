import { AfterViewInit, Component, OnDestroy, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { GetAppoinmentDto } from 'app/_models/getAppoinmentDto';
import { gridSettings } from 'app/_models/grid';
import { AppoinmentsService } from 'app/_services/appoinments.service';
import { AppoinmentsStatuses } from 'Constants';
import { Subject } from 'rxjs';
import { UpdateAppoinmentStatusDto } from 'app/_models/updateAppoinmentStatusDto';
import { ToastrService } from 'ngx-toastr';
import { DataTableDirective } from 'angular-datatables';
import { PacientService } from 'app/_services/pacient.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { MessageService } from 'app/_services/message.service';
import { DoctorAppoinmentsFilterDto } from 'app/_models/_filters/doctorAppoinmentsFilterDto';
import { User } from 'app/_models/user';
import { take } from 'rxjs/operators';
import { AccountService } from 'app/_services/account.service';
import { PresenceService } from 'app/_services/presence.service';

@Component({
  selector: 'app-doctor-appoinments',
  templateUrl: './doctor-appoinments.component.html',
  styleUrls: ['./doctor-appoinments.component.css']
})

export class DoctorAppoinmentsComponent implements OnDestroy, OnInit {
  dtOptions: DataTables.Settings = {};

  @ViewChild(DataTableDirective, { static: false })
  dtElement: DataTableDirective;

  appoinments: GetAppoinmentDto[] = [];
  statuses = AppoinmentsStatuses;

  appoinmentsStatuses = [
    { id: 1, name: 'In asteptare' },
    { id: 2, name: 'Aprobat' },
    { id: 3, name: 'Anulat de pacient' },
    { id: 4, name: 'Anulat de doctor' },
    { id: 5, name: 'Finalizat' },
    { id: 6, name: 'Respins' },
  ];

  // We use this trigger because fetching the list of persons can be quite long,
  // thus we ensure the data is fetched before rendering
  dtTrigger: Subject<any> = new Subject<any>();

  //send message modal
  messageModalRef: BsModalRef;
  receiverUserName: string;
  receiverName: string;
  content: string;

  filterDoctorAppoinments = {} as DoctorAppoinmentsFilterDto;
  dateRange = [];

  user: User;
  isHubConnectionActive = false;

  constructor(private appoinmentService: AppoinmentsService, private pacientService: PacientService,
    private router: Router, private toastr: ToastrService, private messageService: MessageService,
    private modalService: BsModalService, private accountService: AccountService, public presence: PresenceService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
  }

  ngOnInit(): void {
    this.initializeDateRangeWithToday();
    this.initializeGrid();
  }

  initializeGrid() {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 50,
      searching: false,
      columnDefs: [
        { orderable: false, targets: 7 },
      ],
      language: gridSettings,
      order: []
    };
    this.getDateIdsFromDateRange();
    this.appoinmentService.getAppoinmentsForDoctor(this.filterDoctorAppoinments).subscribe(data => {
      this.appoinments = data;
      this.dtTrigger.next();
    });
  };

  ngOnDestroy(): void {
    this.dtTrigger.unsubscribe();
    if (this.isHubConnectionActive) {
      this.messageService.stopHubConnection();
    }
  }

  initializeDateRangeWithToday(){
    this.dateRange = [];
    this.dateRange[0] = new Date();
    this.dateRange[1] = new Date();
  }

  getDateIdsFromDateRange(){
    if (this.dateRange) {
      this.filterDoctorAppoinments.dateFrom = (this.dateRange[0].getFullYear() * 100 + this.dateRange[0].getMonth() + 1) * 100 + this.dateRange[0].getDate();
      this.filterDoctorAppoinments.dateTo = (this.dateRange[1].getFullYear() * 100 + this.dateRange[1].getMonth() + 1) * 100 + this.dateRange[1].getDate();
    }
  }

  redirectToConsultation(appoinment: GetAppoinmentDto) {
    this.router.navigateByUrl('pacient/consultation/' + appoinment.id + "/" + appoinment.pacientFirstName + "/" + appoinment.pacientSecondName);
  }

  updateStatus(newStatusId: number, appoinmentId: number) {
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

      this.getDateIdsFromDateRange();
      this.filterDoctorAppoinments.statusId = this.filterDoctorAppoinments.statusId ?? 0;

      // Destroy the table first
      dtInstance.destroy();

      // Call the dtTrigger to rerender again
      this.appoinmentService.getAppoinmentsForDoctor(this.filterDoctorAppoinments).subscribe(data => {
        this.appoinments = data;
        this.dtTrigger.next();
      });
    });
  }

  goToPacientProfile(id: string) {
    this.router.navigateByUrl('pacient/pacient_profile/' + id);
  }

  openMessageModal(template: TemplateRef<any>, receiverUserName: string, receiverName: string) {
    this.receiverUserName = receiverUserName;
    this.receiverName = receiverName;
    this.messageModalRef = this.modalService.show(
      template,
      Object.assign({}, { class: 'gray modal-lg' })
    );
    if (this.isHubConnectionActive) {
      this.messageService.stopHubConnection();
      this.isHubConnectionActive = false;
    }
    this.messageService.createHubConnection(this.user, this.receiverUserName);
    this.isHubConnectionActive = true;
  }

  sendMessage() {
    this.messageService.sendMessage(this.receiverUserName, this.content)
      .then(() => {
        this.toastr.success(
          '<span data-notify="icon" class="nc-icon nc-bell-55"></span><span data-notify="message">Mesajul a fost trimis cu succes!!</span>',
          "Mesaj",
          {
            toastClass: "alert alert-success alert-with-icon",
          }
        );
        this.content = "";
        this.messageModalRef.hide();
      })
  }

  filterDoctorAppoinmentsList() {
    console.log(this.dateRange);
    this.rerender();
  }

  resetFilter() {
    this.initializeFilterObject();
    this.initializeDateRangeWithToday();
    this.rerender();
  }

  initializeFilterObject() {
    this.filterDoctorAppoinments = {} as DoctorAppoinmentsFilterDto;;
  }






}