import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Pacient } from 'app/_models/pacient';
import { PacientGeneralMedicalDataDto } from 'app/_models/pacientGeneralMedicalDataDto';
import { PacientGeneralDataService } from 'app/_services/pacient-general-data.service';
import { PacientService } from 'app/_services/pacient.service';
import { HighlightSpanKind } from 'typescript';

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
  
  constructor(private route: ActivatedRoute, 
    private pacientService: PacientService,
    private pacientGeneralDataService : PacientGeneralDataService,
    private fb: FormBuilder) 
  { 
    this.pacientId = this.route.snapshot.paramMap.get('id');
  }

  ngOnInit(): void {
    this.getPacientPersonalData();
    this.initializeGeneralDataForm();
  }

  getPacientPersonalData(){
    this.pacientService.getPacientById(this.pacientId).subscribe(response => {
      this.pacient = response;
      this.birthDate = new Date(this.pacient.dateOfBirth);
      this.pacient.pacientGeneralMedicalData.isSmoker = this.pacient.pacientGeneralMedicalData.isSmoker.toString();
      this.pacientGeneralDataForm.patchValue(this.pacient.pacientGeneralMedicalData); 
    })
  }

  initializeGeneralDataForm(){
    this.pacientGeneralDataForm = this.fb.group({
      bloodType: ['', [Validators.required]],
      weightBirth: ['', [Validators.required]],
      heightBirth: ['', [Validators.required]],
      numberOfBirths: ['', [Validators.required]],
      numberOfAvortions: ['', [Validators.required]],
      isSmoker: ["false"],
    });
  }

  saveGeneralData(){

  }



}
