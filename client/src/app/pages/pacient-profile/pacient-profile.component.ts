import { Component, OnDestroy, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ConsultationDto } from 'app/_models/consultationDto';
import { Pacient } from 'app/_models/pacient';
import { PacientGeneralMedicalDataDto } from 'app/_models/pacientGeneralMedicalDataDto';
import { UpdatePacientVaccinesDto } from 'app/_models/updatePacientVaccinesDto';
import { VaccineDto } from 'app/_models/vaccineDto';
import { ConsultationService } from 'app/_services/consultation.service';
import { PacientGeneralDataService } from 'app/_services/pacient-general-data.service';
import { PacientService } from 'app/_services/pacient.service';
import { VaccinesService } from 'app/_services/vaccines.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subject } from 'rxjs';
import { ConsultationFormComponent } from '../consultation-form/consultation-form.component';
import { gridSettings } from 'app/_models/grid';
import { AccountService } from 'app/_services/account.service';
import { take } from 'rxjs/operators';
import { User } from 'app/_models/user';

@Component({
  selector: 'app-pacient-profile',
  templateUrl: './pacient-profile.component.html',
  styleUrls: ['./pacient-profile.component.css']
})

export class PacientProfileComponent implements OnDestroy, OnInit {

  pacientId: string;
  pacient: Pacient;
  birthDate: Date;
  pacientGeneralDataForm: FormGroup;
  pacientGeneralDataDto: PacientGeneralMedicalDataDto;
  validationErrors: string[] = [];

  requiredVaccines: Array<VaccineDto> = [];
  madeVaccines: Array<VaccineDto> = [];

  vaccineModalRef: BsModalRef;
  vaccineDescription: string;
  selectedVaccine: string;

  pacientUpdateDto = {} as UpdatePacientVaccinesDto;

  pacientConsultations: ConsultationDto[] = [];
  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject<any>();

  bloodTypes = [{ name: 'A Rh pozitiv (A+)' },
  { name: 'A Rh negativ (A-)' },
  { name: 'B Rh pozitiv (B+)' },
  { name: 'AB Rh pozitiv (AB+)' },
  { name: 'AB Rh negativ (AB-)' },
  { name: '0 Rh pozitiv (0+)', },
  { name: '0 Rh negativ (0-)', }];

  user: User;

  constructor(private route: ActivatedRoute,
    private pacientService: PacientService,
    private pacientGeneralDataService: PacientGeneralDataService,
    private toastr: ToastrService,
    private fb: FormBuilder,
    private consultationService: ConsultationService,
    private vaccinesService: VaccinesService,
    private router: Router,
    private accountService: AccountService,
    private modalService: BsModalService) {
    this.pacientId = this.route.snapshot.paramMap.get('id');
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
  }

  ngOnInit(): void {
    this.getPacientPersonalData();
    this.initializeGeneralDataForm();
    this.getMadeVaccines();
    this.getRequiredVaccines();
    this.initializeGrid();
  }

  ngOnDestroy(): void {
    this.dtTrigger.unsubscribe();
  }

  getPacientPersonalData() {
    this.pacientService.getPacientById(this.pacientId).subscribe(response => {
      this.pacient = response;
      this.birthDate = new Date(this.pacient.dateOfBirth);
      this.pacientGeneralDataForm.patchValue(this.pacient.pacientGeneralMedicalData);
    })
  }

  initializeGeneralDataForm() {
    this.pacientGeneralDataForm = this.fb.group({
      bloodType: ['', [Validators.required]],
      weightBirth: ['', [Validators.required]],
      heightBirth: ['', [Validators.required]],
      numberOfBirths: ['', [Validators.required]],
      numberOfAvortions: ['', [Validators.required]],
      isSmoker: ["False"],
    });
  }

  saveGeneralData() {
    this.pacientGeneralDataDto = this.pacientGeneralDataForm.value;
    this.pacientGeneralDataDto.pacientId = +this.pacientId;
    this.validationErrors = [];
    this.pacientGeneralDataService.updatePacientGeneralData(this.pacientGeneralDataDto).subscribe(() => {

      this.pacientUpdateDto.vaccines = this.madeVaccines;
      this.pacientUpdateDto.pacientId = +this.pacientId;

      this.vaccinesService.updatePacientVaccines(this.pacientUpdateDto).subscribe();

      this.toastr.success(
        '<span data-notify="icon" class="nc-icon nc-bell-55"></span><span data-notify="message">Modificarile au fost salvate cu succes!!</span>',
        "General",
        {
          toastClass: "alert alert-success alert-with-icon",
        }
      );
    }, error => {
      this.validationErrors = error;
    }
    );
  }

  getRequiredVaccines() {
    this.vaccinesService.getRequiredVaccines().subscribe(response => {
      this.requiredVaccines = response;
    })
  }

  getMadeVaccines() {
    this.vaccinesService.getMadeVaccines(+this.pacientId).subscribe(response => {
      this.madeVaccines = response;
    })
  }

  openVaccineModal(template: TemplateRef<any>) {
    this.vaccineModalRef = this.modalService.show(
      template,
      Object.assign({}, { class: 'gray modal-lg' })
    );
    this.filterMadeVaccinesFromAvailable();
    this.clearVaccineModalInputs();
  }

  addVaccine() {
    if (this.selectedVaccine) {
      this.madeVaccines.push(this.requiredVaccines.find(c => c.id == +this.selectedVaccine));
    }
    this.filterMadeVaccinesFromAvailable();
    this.clearVaccineModalInputs();
    this.vaccineModalRef.hide();
  }

  deleteVaccine(vaccine: VaccineDto) {
    const index: number = this.madeVaccines.indexOf(vaccine);
    if (index !== -1) {
      this.madeVaccines.splice(index, 1);
    }
    this.requiredVaccines.push(vaccine);
  }

  onVaccineChange(e) {
    this.vaccineDescription = this.requiredVaccines.find(c => c.id == e.target.value).description;
  }


  filterMadeVaccinesFromAvailable() {
    this.madeVaccines.forEach(element => {
      this.requiredVaccines = this.requiredVaccines.filter(c => c.id != element.id);
    });
  }

  clearVaccineModalInputs() {
    this.selectedVaccine = null;
    this.vaccineDescription = null;
  }

  initializeGrid() {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 10,
      searching: false,
      columnDefs: [
        { orderable: false, targets: 5 },
        { orderable: false, targets: 10 },
        { orderable: false, targets: 10 },
        { className : "dt-center", targets: "_all"}
      ],
      language: gridSettings,

    };
    this.consultationService.getPacientConsultations(+this.pacientId)
      .subscribe(data => {
        this.pacientConsultations = data;
        this.dtTrigger.next();
      });
  }

  someClickHandler(info: any): void {
    this.router.navigateByUrl('' + info[900]);
  }

  redirectToRecipeForm(consultationId: string){
    this.router.navigateByUrl('pacient/recipe/' + consultationId + "/" + this.pacientId + "/" + this.pacient.firstName + "/" + this.pacient.secondName);
  }

  redirectToRecipePage(consultationId: string){
    this.router.navigateByUrl('pacient/recipe_page/'+ consultationId);
  }

}
