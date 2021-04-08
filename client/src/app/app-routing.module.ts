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

const routes: Routes = [
  {path:'', component: HomeComponent, canActivate: [NoauthGuard]},
  {path:'home', component: HomeComponent, canActivate: [NoauthGuard]},
  {path:'register', component: RegisterComponent, canActivate: [NoauthGuard]},
  {path:'docregister', component: RegisterDoctorComponent, canActivate: [NoauthGuard]},
  {path:'dashboard', component: DashboardComponent},
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
      {path:'table', component: TableComponent},
      {path:'user', component: UserComponent},
      {path:'typography', component: TypographyComponent},
      {path:'icons', component: IconsComponent},
      {path:'maps', component: MapsComponent},
      {path:'notifications', component: NotificationsComponent},
      {path:'upgrade', component: UpgradeComponent}, 
      {path:'doctor_profile', component: DoctorProfileComponent}, 
      {path:'doctor_work_days', component: DoctorWorkDaysComponent}, 
    ]
  },
  {path:'**', component: HomeComponent, canActivate: [NoauthGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
