import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { NgModule } from '@angular/core';
import { ToastrModule } from "ngx-toastr";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SidebarModule } from './sidebar/sidebar.module';
import { FooterModule } from './shared/footer/footer.module';
import { NavbarModule } from './shared/navbar/navbar.module';
import { FixedPluginModule } from './shared/fixedplugin/fixedplugin.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { UserComponent } from './pages/user/user.component';
import { TableComponent } from './pages/table/table.component';
import { TypographyComponent } from './pages/typography/typography.component';
import { IconsComponent } from './pages/icons/icons.component';
import { NotificationsComponent } from './pages/notifications/notifications.component';
import { UpgradeComponent } from './pages/upgrade/upgrade.component';
import { RegisterComponent } from './pages/register/register.component';
import { HomeComponent } from './pages/home/home.component';
import { TextInputComponent } from './_forms/text-input/text-input.component';
import { AppRoutingModule } from "./app-routing.module";
import { BsDatepickerConfig, BsDatepickerModule } from "ngx-bootstrap/datepicker";
import { TimepickerModule } from 'ngx-bootstrap/timepicker';
import { DateInputComponent } from './_forms/date-input/date-input.component';
import { defineLocale } from "ngx-bootstrap/chronos";
import { roLocale } from "ngx-bootstrap/locale";
import { RegisterDoctorComponent } from './pages/register-doctor/register-doctor.component';
import { ErrorInterceptor } from "./interceptors/error.interceptor";
import { TestErrorsComponent } from "./pages/errors/test-errors/test-errors.component";
import { ServerErrorComponent } from "./pages/errors/server-error/server-error.component";
import { NotFoundComponent } from "./pages/errors/not-found/not-found.component";
import { JwtInterceptor } from "./interceptors/jwt.interceptor";
import { DoctorProfileComponent } from './pages/doctor-profile/doctor-profile.component';
import { DoctorWorkDaysComponent } from './pages/doctor-work-days/doctor-work-days.component';
import { NaoDatepickerConfig } from "./adapters/nao-date-picker.adapter";
import { DoctorsListComponent } from './pages/doctors-list/doctors-list.component';
import { DataTablesModule } from "angular-datatables";
import { RomaniandatePipe } from './pipes/romaniandate.pipe';
import { DoctorDetailComponent } from './pages/doctor-detail/doctor-detail.component';
import { TabsModule } from "ngx-bootstrap/tabs";
import { HourPipe } from './pipes/hour.pipe';
import { ModalModule } from 'ngx-bootstrap/modal';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { DoctorAppoinmentsComponent } from './pages/doctor-appoinments/doctor-appoinments.component';
import { PacientAppoinmentsComponent } from './pages/pacient-appoinments/pacient-appoinments.component';
import { ConsultationFormComponent } from './pages/consultation-form/consultation-form.component';
import { PacientProfileComponent } from './pages/pacient-profile/pacient-profile.component';
import { NgxSpinnerModule } from "ngx-spinner";
import { LoadingInterceptor } from "./interceptors/loading.interceptor";
import { PhotoEditorComponent } from './photo-editor/photo-editor.component';
import { FileUploadModule } from "ng2-file-upload";
import { RecipeFormComponent } from './pages/recipe-form/recipe-form.component';
import { NgSelectModule } from '@ng-select/ng-select';
import { RecipePageComponent } from './pages/recipe-page/recipe-page.component';
import { PacientMedicalDataComponent } from './pages/pacient-medical-data/pacient-medical-data.component';
import { MessagesPageComponent } from './pages/messages-page/messages-page.component';
import { TimeagoModule } from "ngx-timeago";
import { AdminComponent } from './pages/admin/admin/admin.component';
import { HasRoleDirective } from "./_directives/has-role.directive";
import { NgxCaptchaModule } from 'ngx-captcha';
import { TestComponent } from './pages/test/test.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { PacientHistoryComponent } from './pacient-history/pacient-history.component';
import { ChartComponent } from './pages/chart/chart.component';
import { VaccineChartComponent } from './pages/vaccine-chart/vaccine-chart.component';
import { RatingModule } from 'ngx-bootstrap/rating';
import { ReviewsComponent } from './pages/reviews/reviews.component';
defineLocale("ro", roLocale);


@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    UserComponent,
    TableComponent,
    TypographyComponent,
    IconsComponent,
    NotificationsComponent,
    RegisterComponent,
    UpgradeComponent,
    TextInputComponent,
    HomeComponent,
    DateInputComponent,
    RegisterDoctorComponent,
    TestErrorsComponent,
    ServerErrorComponent,
    NotFoundComponent,
    DoctorProfileComponent,
    DoctorWorkDaysComponent,
    DoctorsListComponent,
    RomaniandatePipe,
    DoctorDetailComponent,
    HourPipe,
    DoctorAppoinmentsComponent,
    PacientAppoinmentsComponent,
    ConsultationFormComponent,
    PacientProfileComponent,
    PhotoEditorComponent,
    RecipeFormComponent,
    RecipePageComponent,
    PacientMedicalDataComponent,
    MessagesPageComponent,
    AdminComponent,
    TestComponent,
    ResetPasswordComponent,
    ForgotPasswordComponent,
    PacientHistoryComponent,
    ChartComponent,
    VaccineChartComponent,
    ReviewsComponent
  ],
  imports: [
    BrowserAnimationsModule,
    AppRoutingModule,
    SidebarModule,
    NavbarModule,
    ToastrModule.forRoot({
      timeOut: 4000,
      closeButton: true,
      enableHtml: true,
      positionClass: "toast-bottom-right"
    }),
    FooterModule,
    FixedPluginModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BsDatepickerModule.forRoot(),
    TimepickerModule.forRoot(),
    TabsModule.forRoot(),
    DataTablesModule,
    ModalModule.forRoot(),
    BsDropdownModule.forRoot(),
    NgxSpinnerModule,
    FileUploadModule,
    NgSelectModule,
    TimeagoModule.forRoot(),
    NgxCaptchaModule,
    RatingModule.forRoot()
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: BsDatepickerConfig, useFactory: NaoDatepickerConfig }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
