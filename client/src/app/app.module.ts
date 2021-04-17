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
import { MapsComponent } from './pages/maps/maps.component';
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

defineLocale("ro", roLocale);


@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    UserComponent,
    TableComponent,
    TypographyComponent,
    IconsComponent,
    MapsComponent,
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
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: BsDatepickerConfig, useFactory: NaoDatepickerConfig }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
