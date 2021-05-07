import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent }       from './pages/dashboard/dashboard.component';
import { UserComponent }            from './pages/user/user.component';
import { TableComponent }           from './pages/table/table.component';
import { TypographyComponent }      from './pages/typography/typography.component';
import { IconsComponent } from './pages/icons/icons.component';
import { MapsComponent } from './pages/maps/maps.component';
import { NotificationsComponent } from './pages/notifications/notifications.component';
import { UpgradeComponent } from './pages/upgrade/upgrade.component';
import { RegisterComponent } from './pages/register/register.component';
import { AuthGuard } from 'app/guards/auth.guard';
import { NoauthGuard } from './guards/noauth.guard';
import { HomeComponent } from './pages/home/home.component';
import { RegisterDoctorComponent } from './pages/register-doctor/register-doctor.component';
import { TestErrorsComponent } from './pages/errors/test-errors/test-errors.component';
import { ServerErrorComponent } from './pages/errors/server-error/server-error.component';
import { NotFoundComponent } from './pages/errors/not-found/not-found.component';
import { DoctorProfileComponent } from './pages/doctor-profile/doctor-profile.component';
import { DoctorWorkDaysComponent } from './pages/doctor-work-days/doctor-work-days.component';
import { DoctorsListComponent } from './pages/doctors-list/doctors-list.component';
import { DoctorDetailComponent } from './pages/doctor-detail/doctor-detail.component';
import { DoctorAppoinmentsComponent } from './pages/doctor-appoinments/doctor-appoinments.component';
import { PacientAppoinmentsComponent } from './pages/pacient-appoinments/pacient-appoinments.component';
import { ConsultationFormComponent } from './pages/consultation-form/consultation-form.component';
import { PacientProfileComponent } from './pages/pacient-profile/pacient-profile.component';
import { PreventUnsavedChangesGuard } from './guards/prevent-unsaved-changes.guard';

const routes: Routes = [
  {path:'', component: HomeComponent, canActivate: [NoauthGuard]},
  {path:'home', component: HomeComponent, canActivate: [NoauthGuard]},
  {path:'register', component: RegisterComponent, canActivate: [NoauthGuard]},
  {path:'docregister', component: RegisterDoctorComponent, canActivate: [NoauthGuard]},
  {path:'errors', component: TestErrorsComponent},
  {path:'server-error', component: ServerErrorComponent},
  {path:'not-found', component: NotFoundComponent},
  {
    /* protected routes by authentication */
    path:'',
    runGuardsAndResolvers:'always',
    canActivate: [AuthGuard],
    children:
    [
      {path:'dashboard', component: DashboardComponent},
      {path:'table', component: TableComponent},
      {path:'user', component: UserComponent},
      {path:'typography', component: TypographyComponent},
      {path:'icons', component: IconsComponent},
      {path:'maps', component: MapsComponent},
      {path:'notifications', component: NotificationsComponent},
      {path:'upgrade', component: UpgradeComponent}, 
      {path:'doctor_profile', component: DoctorProfileComponent}, 
      {path:'doctor_work_days', component: DoctorWorkDaysComponent}, 
      {path:'doctors', component: DoctorsListComponent}, 
      {path:'doctor/detail/:id', component: DoctorDetailComponent}, 
      {path:'doctor/appoinments', component: DoctorAppoinmentsComponent}, 
      {path:'pacient/appoinments', component: PacientAppoinmentsComponent}, 
      {path:'pacient/consultation/:appoinmentId/:pacientFirstName/:pacientSecondName', component: ConsultationFormComponent, canDeactivate : [PreventUnsavedChangesGuard]}, 
      {path:'pacient/pacient_profile/:id', component: PacientProfileComponent}, 
    ]
  },
  {path:'**', component: HomeComponent, canActivate: [NoauthGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
