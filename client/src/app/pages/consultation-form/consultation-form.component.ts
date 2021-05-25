import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';
import { Component, HostListener, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ConsultationDto } from 'app/_models/consultationDto';
import { ConsultationService } from 'app/_services/consultation.service';
import { ToastrService } from 'ngx-toastr';

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
  consultation: ConsultationDto;

  @HostListener('window:beforeunload', ['$event']) unloadNotification($event : any){
    if(this.consultationForm.dirty){
      $event.returnValue = true;
    }
  }

  generalFeelingTypes = [
    { id: 1, name: 'Foarte buna' },
    { id: 2, name: 'Buna' },
    { id: 3, name: 'Extenuat' },
    { id: 4, name: 'Obosit' },
    { id: 5, name: 'Suparat' },
    { id: 6, name: 'Plictisit' },
    { id: 7, name: 'Depresiv' },
    { id: 8, name: 'Anxios' },
    { id: 9, name: 'Febril' },
  ];

  constructor(private consultationService: ConsultationService, private fb: FormBuilder, 
    private router: Router, 
    private route: ActivatedRoute,
    private toastr: ToastrService) 
    {
      this.appoinmentId = this.route.snapshot.paramMap.get('appoinmentId');
      this.pacientFirstName = this.route.snapshot.paramMap.get('pacientFirstName');
      this.pacientSecondName = this.route.snapshot.paramMap.get('pacientSecondName');
     }

  ngOnInit(): void 
  {
    this.initializeForm();
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
      appoinmentId: ['']
    });
  }

  addConsultation(){
    this.consultationForm.value.appoinmentId = +this.appoinmentId;
    this.consultation = this.consultationForm.value;
    this.consultationForm.markAsPristine();
    this.consultationService.addConsultation(this.consultation).subscribe(response => {
      console.log(response);
      this.toastr.success(
        '<span data-notify="icon" class="nc-icon nc-bell-55"></span><span data-notify="message">Consultatia a fost adaugata cu succes!!</span>',
        "Consultatie",
        {
          toastClass: "alert alert-success alert-with-icon",
        }
      );
      this.router.navigateByUrl('/doctor/appoinments');
    }, error => {
      this.validationErrors = error;
    }
    )
  }

  cancel() {
    this.consultationForm.reset();
  }

}
