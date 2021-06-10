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

@Component({
  selector: 'app-doctor-appoinments',
  templateUrl: './doctor-appoinments.component.html',
  styleUrls: ['./doctor-appoinments.component.css']
})



export class DoctorAppoinmentsComponent implements OnDestroy, OnInit {
  dtOptions: DataTables.Settings = {};

  @ViewChild(DataTableDirective, {static: false})
  dtElement: DataTableDirective;
  
  appoinments: GetAppoinmentDto[] = [];
  statuses = AppoinmentsStatuses;

  // We use this trigger because fetching the list of persons can be quite long,
  // thus we ensure the data is fetched before rendering
  dtTrigger: Subject<any> = new Subject<any>();

  //send message modal
  messageModalRef: BsModalRef;
  receiverUserName: string;
  receiverName: string;
  content: string;

  constructor(private appoinmentService: AppoinmentsService, private pacientService: PacientService, 
    private router : Router, private toastr: ToastrService, private messageService: MessageService,
    private modalService: BsModalService) { }

  ngOnInit(): void {
    this.initializeGrid();
  }

  initializeGrid() {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 10,
      columnDefs: [
        { orderable: false, targets: 0 },
        { orderable: false, targets: 7 },
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
      this.appoinmentService.getAppoinmentsForDoctor().subscribe(data => {
        this.appoinments = data;
        this.dtTrigger.next();
      });
    });
  }

  goToPacientProfile(id: string){
    this.router.navigateByUrl('pacient/pacient_profile/' + id);
  }

  openMessageModal(template: TemplateRef<any>, receiverUserName: string, receiverName: string) {
    this.receiverUserName = receiverUserName;
    this.receiverName = receiverName;
    this.messageModalRef = this.modalService.show(
      template,
      Object.assign({}, { class: 'gray modal-lg' })
    );
  }

  sendMessage() {
    this.messageService.sendMessage(this.receiverUserName, this.content)
      .subscribe(() => {
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
}