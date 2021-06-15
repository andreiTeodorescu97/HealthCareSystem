import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from 'app/_services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {

  forgotPasswordForm: FormGroup;
  validationErrors: string[] = [];
  model: any = {};

  constructor(private accountService: AccountService,
    private toastrService: ToastrService,
    private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  sendEmail() {
    this.model.email = this.forgotPasswordForm.value.email;
    this.accountService.forgotPassword(this.model).subscribe(() => {
      this.toastrService.success(
        '<span data-notify="icon" class="nc-icon nc-bell-55"></span><span data-notify="message">Un email a fost trimis catre ' + this.model.email + '!</span>',
        "Resetare parola",
        {
          toastClass: "alert alert-success alert-with-icon",
        }
      );
      this.router.navigateByUrl('/home');
    }, (error) => {
      this.validationErrors = error;
      this.toastrService.error(
        '<span data-notify="icon" class="nc-icon nc-bell-55"></span><span data-notify="message">Nu am putut trimite email catre ' + this.model.email + '!</span>',
        "Resetare parola",
        {
          toastClass: "alert alert-danger alert-with-icon",
        }
      );
    })
  }

  initializeForm() {
    this.forgotPasswordForm = this.fb.group({
      email: ['', [Validators.required, Validators.pattern('^[^\\s@]+@[^\\s@]+\\.[^\\s@]{2,}$')]],
    });
  }



}
