import { Component, OnDestroy, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { DataTableDirective } from 'angular-datatables';
import { GetAppoinmentDto } from 'app/_models/getAppoinmentDto';
import { gridSettings } from 'app/_models/grid';
import { UpdateAppoinmentStatusDto } from 'app/_models/updateAppoinmentStatusDto';
import { User } from 'app/_models/user';
import { AddReviewDto } from 'app/_models/_reviews/AddReviewDto';
import { AccountService } from 'app/_services/account.service';
import { AppoinmentsService } from 'app/_services/appoinments.service';
import { MessageService } from 'app/_services/message.service';
import { PresenceService } from 'app/_services/presence.service';
import { ReviewService } from 'app/_services/review.service';
import { AppoinmentsStatuses } from 'Constants';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subject } from 'rxjs';
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-pacient-appoinments',
  templateUrl: './pacient-appoinments.component.html',
  styleUrls: ['./pacient-appoinments.component.css']
})
export class PacientAppoinmentsComponent implements OnDestroy, OnInit {

  dtOptions: DataTables.Settings = {};
  appoinments: GetAppoinmentDto[] = [];
  statuses = AppoinmentsStatuses;

  @ViewChild(DataTableDirective, { static: false })
  dtElement: DataTableDirective;
  // We use this trigger because fetching the list of persons can be quite long,
  // thus we ensure the data is fetched before rendering
  dtTrigger: Subject<any> = new Subject<any>();

  //send message modal
  messageModalRef: BsModalRef;
  receiverUserName: string;
  receiverName: string;
  receiverProfileImg: string;
  content: string;

  user: User;
  isHubConnectionActive = false;

  reviewDoctorFullName: string;
  reviewModalRef: BsModalRef;
  reviewDoctorProfileImg: string;
  reviewDoctorRating: number;
  review = {} as AddReviewDto;

  constructor(private appoinmentService: AppoinmentsService,
    private messageService: MessageService,
    private modalService: BsModalService,
    private toastr: ToastrService, public presence: PresenceService, private accountService: AccountService,
    private reviewService: ReviewService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
  }

  ngOnInit(): void {
    this.initializeGrid();
  }

  initializeGrid() {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 50,
      columnDefs: [
        { orderable: false, targets: 0 },
        { orderable: false, targets: 5 },
      ],
      language: gridSettings,
      order: []
    };
    this.appoinmentService.getAppoinmentsForPacient().subscribe(data => {
      this.appoinments = data;
      this.dtTrigger.next();
    });
  };

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
    if (this.isHubConnectionActive) {
      this.messageService.stopHubConnection();
    }
  }

  openMessageModal(template: TemplateRef<any>, receiverUserName: string, receiverName: string,
    receiverProfileImg: string) {
    this.receiverUserName = receiverUserName;
    this.receiverName = receiverName;
    this.receiverProfileImg = receiverProfileImg;
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

  openReviewModal(template: TemplateRef<any>, appoinment: GetAppoinmentDto) {
    this.reviewDoctorFullName = "Dr. " + appoinment.doctorFirstName + " " + appoinment.doctorSecondName;
    this.reviewDoctorProfileImg = appoinment.doctorProfilePhotoUrl;
    this.review.rating = 0;
    this.review.content = null;
    this.review.doctorId = appoinment.doctorId;
    this.reviewModalRef = this.modalService.show(
      template,
      Object.assign({}, { class: 'gray modal-lg' })
    );
  }

  addReview() {
    this.reviewService.addReview(this.review).subscribe(() => {
      this.toastr.success(
        '<span data-notify="icon" class="nc-icon nc-bell-55"></span><span data-notify="message">Recenzia a fost adaugata cu succes!!</span>',
        "Recenzie",
        {
          toastClass: "alert alert-success alert-with-icon",
        }
      );
      this.reviewModalRef.hide();
    })
  }
}