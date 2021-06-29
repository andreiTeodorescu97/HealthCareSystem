import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, FormGroupDirective, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { DoctorDto } from 'app/_models/doctorDto';
import { FreeHourDto } from 'app/_models/freehourDto';
import { MakeAnAppoinmentDto } from 'app/_models/makeAnAppoinmentDto';
import { ReviewDto } from 'app/_models/_reviews/ReviewDto';
import { AppoinmentsService } from 'app/_services/appoinments.service';
import { DoctorService } from 'app/_services/doctor.service';
import { ReviewService } from 'app/_services/review.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-doctor-detail',
  templateUrl: './doctor-detail.component.html',
  styleUrls: ['./doctor-detail.component.css']
})
export class DoctorDetailComponent implements OnInit {

  doctor: DoctorDto;
  chooseDateForm: FormGroup;
  minDate: Date;
  doctorId: string;
  availableHours: Array<FreeHourDto> = [];
  @ViewChild('documentEditForm') documentEditForm: FormGroupDirective;

  modalRef: BsModalRef;
  @ViewChild('template') templateRef: TemplateRef<any>;

  appoinmentDto = {} as MakeAnAppoinmentDto;
  reason: string;
  hour: FreeHourDto;
  unixTime: number;

  staticModalFdReference: BsModalRef;
  reviews: ReviewDto[];

  reasonTypes = [
    { id: 1, name: 'Control rutina' },
    { id: 2, name: 'Dureri acute' },
    { id: 3, name: 'Raceala' },
    { id: 4, name: 'Concediu' },
    { id: 5, name: 'Trimitere' },
    { id: 6, name: 'Reteta' },
    { id: 7, name: 'Adeverinte' },
    { id: 8, name: 'Vaccin' },
    { id: 9, name: 'Altul' },
  ];

  constructor(private doctorService: DoctorService, private route: ActivatedRoute, private fb: FormBuilder,
    private modalService: BsModalService,
    private appoinmentsService: AppoinmentsService,
    private toastr: ToastrService,
    private reviewService: ReviewService,
    private router: Router) {
    this.doctorId = this.route.snapshot.paramMap.get('id');
  }

  ngOnInit(): void {
    this.loadMember();
    this.initializeForm();
    this.minDate = new Date();
    this.getDoctorReviews();
  }

  loadMember() {
    this.doctorService.getDoctorByDoctorId(this.doctorId).subscribe(response => {
      this.doctor = response;
      this.doctor.studiesAndExperience.forEach(study => {
        study.startDate = new Date(study.startDate);
        study.endDate = new Date(study.endDate);
      });
      this.doctor.workDays.forEach(workDay => {
        workDay.startHour = new Date(workDay.startHour);
        workDay.endHour = new Date(workDay.endHour);
      });
    })
  }

  initializeForm() {
    this.chooseDateForm = this.fb.group({
      dateOfAppoinment: ['', [Validators.required]]
    })
  }

  getFreeHours() {
    this.unixTime = new Date(this.chooseDateForm.value.dateOfAppoinment).getTime() / 1000;
    this.appoinmentsService.getAvailableHours(this.doctorId, this.unixTime).toPromise().then(response => {
      this.availableHours = response == null ? null : response;
    });
    this.openModal(this.templateRef);
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }


  makeAppoinment() {
    if(this.hour == undefined){
      this.toastr.warning(
        '<span data-notify="icon" class="nc-icon nc-bell-55"></span><span data-notify="message">Nu exista locuri libere la aceasta data!</span>',
        "Programare",
        {
          toastClass: "alert alert-warning alert-with-icon",
        }
      );
      this.modalRef.hide();
    }
    this.appoinmentDto.doctorId = +this.doctorId;
    this.appoinmentDto.reason = this.reason;
    this.appoinmentDto.dayUnixTime = this.unixTime;
    this.appoinmentDto.fromTimeSpan = this.availableHours[+this.hour].fromTimeSpan;
    this.appoinmentDto.toTimeSpan = this.availableHours[+this.hour].toTimeSpan;

    this.appoinmentsService.makeAnAppoinment(this.appoinmentDto).subscribe(() => {
      this.toastr.success(
        '<span data-notify="icon" class="nc-icon nc-bell-55"></span><span data-notify="message">Programarea a fost inregistrata cu succes! Veti primi prin e-mail mesajul de aprobare!</span>',
        "Programare",
        {
          toastClass: "alert alert-success alert-with-icon",
        }
      );
      this.modalRef.hide();
    })
  }

  getDoctorReviews(){
    this.reviewService.getReviewsForDoctor(+this.doctorId).subscribe(data => {
      this.reviews = data;
    });
  };

}
