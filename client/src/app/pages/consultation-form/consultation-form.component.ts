import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ConsultationService } from 'app/_services/consultation.service';

@Component({
  selector: 'app-consultation-form',
  templateUrl: './consultation-form.component.html',
  styleUrls: ['./consultation-form.component.css']
})
export class ConsultationFormComponent implements OnInit {

  consultationForm: FormGroup;
  validationErrors: string[] = [];
  appoinmentId : string;
  pacientFirstName: string;
  pacientSecondName: string;

  constructor(private consultationService: ConsultationService, private fb: FormBuilder, 
    private router: Router, 
    private route: ActivatedRoute) {
      this.appoinmentId = this.route.snapshot.paramMap.get('appoinmentId');
      this.pacientFirstName = this.route.snapshot.paramMap.get('pacientFirstName');
      this.pacientSecondName = this.route.snapshot.paramMap.get('pacientSecondName');
     }

  ngOnInit(): void 
  {
    this.initializeForm();
  }

  addConsultation(){

  }

  initializeForm(){
    this.consultationForm = this.fb.group({
      height: ['', [Validators.required]],
      weight: ['', [Validators.required]],
      systolicBp: [''],
      diastolicBp: [''],
      temperature: ['', [Validators.required]],
      heartRate: ['', [Validators.required]],
      bloodSugar: [''],
      bmi: [''],
      respiratoryRate: [''],
      numberOfCigarettesPerDay: [''],
      generalFeeling: [''],
      comments: [''],
    });
  }

  cancel() {
    this.consultationForm.reset();
  }

}
