import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from 'app/_services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  model: any = {};
  registerForm: FormGroup;

  constructor(private accountService: AccountService, private toastr: ToastrService, private router: Router, private fb: FormBuilder) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.registerForm = this.fb.group({
      username: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(15)]],
      password: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(15)]],
      confirmPassword: ['', [Validators.required, this.matchValues('password')]],
      firstName: ['', [Validators.required]],
      secondName: ['', [Validators.required]],
      email: ['', [Validators.required]],
      identityNumber: ['', [Validators.required]],
      series: ['', [Validators.required]],
      cnp: ['', [Validators.required]],
      dateOfBirth: ['', [Validators.required]],
      gender:['masculin']
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
    console.log(this.registerForm.value);
    /*     this.model.IsPacientAccount = true;
        this.accountService.register(this.model).subscribe(response => {
          console.log(response);
          this.toastr.success(
            '<span data-notify="icon" class="nc-icon nc-bell-55"></span><span data-notify="message">Contul a fost creat cu succes!</span>',
            "Register",
            {
              toastClass: "alert alert-success alert-with-icon",
            }
          );
          this.router.navigateByUrl('/dashboard');
        }, error => {
          console.log(error);
          this.toastr.error(
            '<span data-notify="icon" class="nc-icon nc-bell-55"></span><span data-notify="message">' + error.error + '</span>',
            "Register",
            {
              toastClass: "alert alert-danger alert-with-icon",
            }
          )
        }
        ) */
  }

  cancel() {
    console.log('cancelled');
  }

}
