import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { RegisterDto } from 'app/_models/registerDto';
import { AccountService } from 'app/_services/account.service';
import { environment } from 'environments/environment';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register-doctor',
  templateUrl: './register-doctor.component.html',
  styleUrls: ['./register-doctor.component.css']
})
export class RegisterDoctorComponent implements OnInit {
  model: any = {};
  registerForm: FormGroup;
  maxDate: Date;
  registerDto = { doctorDto: {} } as RegisterDto;
  validationErrors: string[] = [];
  siteKey: string;

  constructor(private accountService: AccountService, private toastr: ToastrService, private router: Router, private fb: FormBuilder) { }

  ngOnInit(): void {
    this.initializeForm();
    this.maxDate = new Date();
    this.siteKey = environment.googleCaptchaSiteKey;
  }

  initializeForm() {
    this.registerForm = this.fb.group({
      username: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(15)]],
      password: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(15), 
        Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%?&])[A-Za-z\d$@$!%?&]{8,}$/)]],
      confirmPassword: ['', [Validators.required, this.matchValues('password')]],
      firstName: ['', [Validators.required]],
      secondName: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.pattern('^[^\\s@]+@[^\\s@]+\\.[^\\s@]{2,}$')]],
      dateOfBirth: ['', [Validators.required]],
      recaptcha: ['', Validators.required]
    })

    this.registerForm.controls.password.valueChanges.subscribe(() => {
      this.registerForm.controls.confirmPassword.updateValueAndValidity();
    })
  }

  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control?.value === control?.parent?.controls[matchTo].value ? null : { isMatching: true }
    }
  }

  register() {
    this.model.IsPacientAccount = true;
    this.registerDto.username = this.registerForm.value.username;
    this.registerDto.password = this.registerForm.value.password;
    this.registerDto.isPacientAccount = false;
    this.registerDto.doctorDto.firstName = this.registerForm.value.firstName;
    this.registerDto.doctorDto.secondName = this.registerForm.value.secondName;
    this.registerDto.doctorDto.email = this.registerForm.value.email;
    this.registerDto.doctorDto.dateOfBirth = this.registerForm.value.dateOfBirth;

    this.accountService.register(this.registerDto).subscribe(() => {
      this.toastr.success(
        '<span data-notify="icon" class="nc-icon nc-bell-55"></span><span data-notify="message">Contul a fost creat cu succes! Un email a fost trimis la adresa introdusa!</span>',
        "Inregistrare",
        {
          toastClass: "alert alert-success alert-with-icon",
        }
      );
      this.router.navigateByUrl('/home');
    }, error => {
      this.validationErrors = error;
    }
    )
  }

  cancel() {
    this.registerForm.reset();
  }
}
