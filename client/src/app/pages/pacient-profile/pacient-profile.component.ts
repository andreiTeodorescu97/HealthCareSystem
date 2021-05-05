import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Pacient } from 'app/_models/pacient';
import { PacientGeneralMedicalDataDto } from 'app/_models/pacientGeneralMedicalDataDto';
import { VaccineDto } from 'app/_models/vaccineDto';
import { PacientGeneralDataService } from 'app/_services/pacient-general-data.service';
import { PacientService } from 'app/_services/pacient.service';
import { VaccinesService } from 'app/_services/vaccines.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-pacient-profile',
  templateUrl: './pacient-profile.component.html',
  styleUrls: ['./pacient-profile.component.css']
})
export class PacientProfileComponent implements OnInit {

  pacientId: string;
  pacient: Pacient;
  birthDate: Date;
  pacientGeneralDataForm: FormGroup;
  pacientGeneralDataDto: PacientGeneralMedicalDataDto;
  validationErrors: string[] = [];

  requiredVaccines: Array<VaccineDto> = [];
  madeVaccines: Array<VaccineDto> = [];


  bloodTypes = [{ name: 'A Rh pozitiv (A+)' },
  { name: 'A Rh negativ (A-)' },
  { name: 'B Rh pozitiv (B+)' },
  { name: 'AB Rh pozitiv (AB+)' },
  { name: 'AB Rh negativ (AB-)' },
  { name: '0 Rh pozitiv (0+)', },
  { name: '0 Rh negativ (0-)', }];

  constructor(private route: ActivatedRoute,
    private pacientService: PacientService,
    private pacientGeneralDataService: PacientGeneralDataService,
    private toastr: ToastrService,
    private fb: FormBuilder,
    private vaccinesService: VaccinesService) {
    this.pacientId = this.route.snapshot.paramMap.get('id');
  }

  ngOnInit(): void {
    this.getPacientPersonalData();
    this.initializeGeneralDataForm();
    this.getRequiredVaccines();
    this.getMadeVaccines();
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
    this.pacientGeneralDataService.updatePacientGeneralData(this.pacientGeneralDataDto).subscribe(response => {
      console.log(response);
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

  openVaccineModal(){
    
  }

  deleteVaccine(vaccineId: number){

  }



}
